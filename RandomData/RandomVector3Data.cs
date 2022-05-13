using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Framework
{
    [System.Serializable]
    public class RandomVector3Data : AbstractRandomData<Vector3>
    {
        public override Vector3 GetRandomValue()
        {
            float x = useSeed ? Rng.RollRandomFloatInRange(minValue.x, maxValue.x) : UnityEngine.Random.Range(minValue.x, maxValue.x);
            float y = useSeed ? Rng.RollRandomFloatInRange(minValue.y, maxValue.y) : UnityEngine.Random.Range(minValue.y, maxValue.y);
            float z = useSeed ? Rng.RollRandomFloatInRange(minValue.z, maxValue.z) : UnityEngine.Random.Range(minValue.z, maxValue.z);

            return new Vector3(x, y, z);
        }

        public override Vector3 GetRandomValue(Vector3 _minValue, Vector3 _maxValue)
        {
            float x = useSeed ? Rng.RollRandomFloatInRange(_minValue.x, _maxValue.x) : UnityEngine.Random.Range(_minValue.x, _maxValue.x);
            float y = useSeed ? Rng.RollRandomFloatInRange(_minValue.y, _maxValue.y) : UnityEngine.Random.Range(_minValue.y, _maxValue.y);
            float z = useSeed ? Rng.RollRandomFloatInRange(_minValue.z, _maxValue.z) : UnityEngine.Random.Range(_minValue.z, _maxValue.z);

            return new Vector3(x, y, z);
        }
    }
}