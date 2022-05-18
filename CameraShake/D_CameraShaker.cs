using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Framework
{
    /// <summary>
    /// Implementation of Camera Shaker which allows shaking using Noise in addition to Random numbers, which makes 
    /// resulting effect more pleasing to an eye.
    /// </summary>

    public class D_CameraShaker : Singleton<D_CameraShaker>
    {
        [SerializeField] private Transform targetCamera = null;

        [SerializeField] private Vector3 maxShake = Vector3.one * 15;
        [SerializeField] private int seed = 0;

        [SerializeField] private ShakeMethod shakeMethod = ShakeMethod.Noise;
        [SerializeField] private ShakeReductionType shakeReductionType = ShakeReductionType.Quadratic;

        [SerializeField] private float magnitudeReduction = .5f;
        [SerializeField] private float noiseSpeed = .2f;

        private bool isShaking = false;
        private float trauma = 0;
        private float initialMagnitudeReduction = 0f;
        private Vector3 noiseOffset = Vector3.zero;

        private D_Rng rng;

        protected override void Awake()
        {
            base.Awake();

            rng = new D_Rng(seed);
            initialMagnitudeReduction = magnitudeReduction;
        }

        public void AddShake(float magnitude)
        {
            trauma += magnitude;
            trauma = Mathf.Clamp01(trauma);

            if (!isShaking)
            {
                StartCoroutine(ShakeRoutine());
            }
        }

        public void AddClampShake(float magnitude, float max)
        {
            if (trauma >= max)
                return;

            if (max - trauma < magnitude)
            {
                AddShake(max - magnitude);
            }
            else
            {
                AddShake(magnitude);
            }
        }

        public void AddLimitedShake(float magnitude)
        {
            if (trauma >= magnitude)
                return;

            AddShake(magnitude - trauma);
        }

        public void SetShake(float magnitude)
        {
            magnitude = Mathf.Clamp01(magnitude);
            trauma = 0;
            AddShake(magnitude);
        }

        public void ShakeFor(float magnitude, float duration)
        {
            magnitudeReduction = initialMagnitudeReduction;
            float traumaReductionRate = magnitudeReduction;
            magnitudeReduction = 0;

            AddShake(magnitude);
            MB_Utilities.Execute(duration, () => 
                magnitudeReduction = traumaReductionRate
            );
        }

        /*public void AddShakeByDistance(Vector3 origin, DistanceShake distanceShake)
        {
            if (distanceShake.minMaxDis.x > distanceShake.minMaxDis.y)
            {
                Debug.LogError("Min distance has to be less than the max in the DistanceShake values.");
                return;
            }
            else if (distanceShake.minMagnitude > distanceShake.maxMagnitude)
            {
                Debug.LogError("Min shake has to be less than the max in the DistanceShake values.");
                return;
            }

            if (distanceShake.minMaxDis.y == 0)
                return;

            float value = (targetCamera.position - origin).magnitude;                              // calculate distance to origin
            value = Mathf.Clamp(value, distanceShake.minMaxDis.x, distanceShake.minMaxDis.y);   // clamp the distance between min and max
            value.Remap(distanceShake.minMaxDis.x, distanceShake.minMaxDis.y, 0, 1);   // remap that distance to be between 0 and 1
            value = distanceShake.falloffCurve.Evaluate(value);                                 // evaluate the shake fallof curve at that point
            value.Remap(0, 1, distanceShake.minMagnitude, distanceShake.maxMagnitude);            // remap again between min and max shake to have the magnitude to add

            AddLimitedShake(value);
        }*/

        private IEnumerator ShakeRoutine()
        {
            Quaternion originalRot = targetCamera.localRotation;

            noiseOffset.Set(0f, 20f, 40f);

            isShaking = true;

            while (trauma > 0)
            {
                targetCamera.localRotation = originalRot;

                Shake_Tick(Vector3.right);
                Shake_Tick(Vector3.up);
                Shake_Tick(Vector3.forward);

                noiseOffset += Vector3.one * noiseSpeed;

                trauma -= magnitudeReduction * Time.deltaTime;
                yield return Yield_Utilities.FixedUpdate;
            }

            isShaking = false;
            targetCamera.localRotation = originalRot;
        }

        private void Shake_Tick(Vector3 axis)
        {
            float dir = EvaluateDirection(shakeMethod, Vector3.Scale(noiseOffset, axis).magnitude);

            targetCamera.localRotation = Quaternion.Euler(targetCamera.localRotation.eulerAngles +
                (axis.normalized * Vector3.Scale(maxShake, axis).magnitude * EvaluateShake(shakeReductionType) * dir));
        }

        private float EvaluateDirection(ShakeMethod _shakeMethod, float noiseCoordinates)
        {
            return _shakeMethod switch
            {
                ShakeMethod.Noise => Mathf.PerlinNoise(noiseCoordinates, noiseCoordinates).Remap(0, 1, -1, 1),
                ShakeMethod.Random => rng.RollRandomFloatMinusOneToOne(),
                _ => 0f,
            };
        }

        private float EvaluateShake(ShakeReductionType _shakeRelationType)
        {
            return _shakeRelationType switch
            {
                ShakeReductionType.Linear => trauma,
                ShakeReductionType.Quadratic => trauma * trauma,
                ShakeReductionType.Cubic => trauma * trauma * trauma,
                _ => 0f,
            };
        }

        public enum ShakeReductionType
        {
            Linear,
            Quadratic,
            Cubic
        }

        public enum ShakeMethod
        {
            Noise,
            Random
        }

/*        [System.Serializable]
        public struct DistanceShake
        {
            [Tooltip("Min and Max distance to have in count.")]
            public Vector2 minMaxDis;

            [Tooltip("Lower shake magnitude that can be added.")]
            [Range(0, 1)] public float minMagnitude;
            [Tooltip("Greater shake magnitude that can be added.")]
            [Range(0, 1)] public float maxMagnitude;

            [Tooltip("Curve that defines how the shake is reduce by the distance.\n" +
                "The X axis is distance. 0 = minDis and 1 = maxDis.\n" +
                "The Y axis is magnitude. 0 = minShake and 1 = maxShake.")]
            public AnimationCurve falloffCurve;

            public DistanceShake(float setMinDis, float setMaxDis, float setMinMagnitude, float setMaxMagnitude)
            {
                minMaxDis.x = setMinDis;
                minMaxDis.y = setMaxDis;
                minMagnitude = setMinMagnitude;
                maxMagnitude = setMaxMagnitude;
                falloffCurve = new AnimationCurve();
                falloffCurve.AddKey(0, 1);      // make the falloff linear by default
                falloffCurve.AddKey(1, 0);
            }
        }*/


        [Header("Test")]
        public float m_testShakeAmount = .2f;
        public float m_testShakeDuration = 1f;

        [D_NaughtyAttributes.Button]
        private void ShakeForTest()
        {
            ShakeFor(m_testShakeAmount, m_testShakeDuration);
        }

        [D_NaughtyAttributes.Button]
        private void Shake()
        {
            //AddClampShake(m_testShakeAmount, .5f);
            AddShake(m_testShakeAmount);
        }
    }
}

/*
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Utilities
{
    public class D_CameraShake : MonoBehaviour
    {
        [SerializeField] private int seed = default;

        [SerializeField] private float traumaDecreaseRate = 0.01f;
        //[SerializeField] private Vector3 maxOffset = Vector3.one;
        [SerializeField] private Vector3 maxRotation = Vector3.one * 30;

        [SerializeField] private float traumaAdd = .2f;
        private D_Rng rng;

        private float traumaValue;
        private float shakeValue;
        private ShakeRelationType shakeRelationType = ShakeRelationType.Quadratic;

        //private Vector3 positionOffsetVector;
        private Vector3 rotationOffsetVector;

        private Vector3 initialRotation;

        private void Awake()
        {
            rng = new D_Rng(seed);
            initialRotation = transform.rotation.eulerAngles;
        }

        private void LateUpdate()
        {

            traumaValue = Mathf.Clamp01(traumaValue - traumaDecreaseRate);
            shakeValue = CalculateShakeValue(shakeRelationType);

            SetRandomOffsets();

            //transform.position = transform.position + positionOffsetVector;
            transform.rotation = Quaternion.Euler(initialRotation + rotationOffsetVector);
        }

        private void SetRandomOffsets()
        {
            rotationOffsetVector.Set(
                maxRotation.x * shakeValue * rng.RollRandomFloatMinusOneToOne(),
                maxRotation.y * shakeValue * rng.RollRandomFloatMinusOneToOne(),
                maxRotation.z * shakeValue * rng.RollRandomFloatMinusOneToOne()
                );

            positionOffsetVector.Set(
                maxOffset.x * shakeValue * rng.RollRandomFloatMinusOneToOne(),
                maxOffset.y * shakeValue * rng.RollRandomFloatMinusOneToOne(),
                maxOffset.z * shakeValue * rng.RollRandomFloatMinusOneToOne()
                );
        }

        private void SetNoiseOffsets()
{
    float currentTime = Time.time;

    rotationOffsetVector.Set(
        maxRotation.x * shakeValue * Mathf.PerlinNoise(seed, currentTime).Remap(0f, 1f, -1f, 1f),
        maxRotation.y * shakeValue * Mathf.PerlinNoise(seed + 1, currentTime).Remap(0f, 1f, -1f, 1f),
        maxRotation.z * shakeValue * Mathf.PerlinNoise(seed + 2, currentTime).Remap(0f, 1f, -1f, 1f)
        );

                positionOffsetVector.Set(
                    maxOffset.x * shakeValue * Mathf.PerlinNoise(seed + 3, currentTime).Remap(0f, 1f, -1f, 1f),
                    maxOffset.y * shakeValue * Mathf.PerlinNoise(seed + 4, currentTime).Remap(0f, 1f, -1f, 1f),
                    maxOffset.z * shakeValue * Mathf.PerlinNoise(seed + 5, currentTime).Remap(0f, 1f, -1f, 1f)
                    );
}

private float CalculateShakeValue(ShakeRelationType shakeRelationType)
{
    switch (shakeRelationType)
    {
        case ShakeRelationType.Quadratic:
            return traumaValue * traumaValue;
        case ShakeRelationType.Cubic:
            return traumaValue * traumaValue * traumaValue;
        default:
            return 0f;
    }
}

[D_NaughtyAttributes.Button]
private void Test()
{
    traumaValue += traumaAdd;

}

[SerializeField] private float targetTimeScale = 1f;

[D_NaughtyAttributes.Button]
private void ChangeTimeScale()
{
    Time.timeScale = targetTimeScale;
}
    }
}
 */