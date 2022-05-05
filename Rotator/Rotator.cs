using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Utilities
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private bool rotateBetweenValues = false;

        [SerializeField] [D_NaughtyAttributes.ShowIf("rotateBetweenValues")]
        private Vector3 minRotationVector = default;

        [SerializeField] [D_NaughtyAttributes.ShowIf("rotateBetweenValues")]
        private Vector3 maxRotationVector = default;

        [SerializeField] [D_NaughtyAttributes.ShowIf("rotateBetweenValues", inverted: true)]
        private Vector3 rotationVector = default;

        [SerializeField] private float speed = 1f;

        [SerializeField] private RandomFloatData randomData = null;

        private Vector3 rotationValue;

        private void Start()
        {
            rotationValue = rotateBetweenValues ?
                new Vector3(
                    randomData.GetRandomValue(minRotationVector.x, maxRotationVector.x),
                    randomData.GetRandomValue(minRotationVector.y, maxRotationVector.y),
                    randomData.GetRandomValue(minRotationVector.z, maxRotationVector.z)
                ) : rotationVector;
        }

        private void Update()
        {
            transform.Rotate(rotationValue * speed * Time.deltaTime);
        }
    }
}