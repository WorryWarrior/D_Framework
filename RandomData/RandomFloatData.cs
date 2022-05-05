using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Utilities
{
    [System.Serializable]
    public class RandomFloatData : AbstractRandomData<float>
    {
        public override float GetRandomValue()
        {
            float value = useSeed ? (float)Rng.NextDouble(minValue, maxValue) : UnityEngine.Random.Range(minValue, maxValue);
            return value;
        }

        public override float GetRandomValue(float _minValue, float _maxValue)
        {
            float value = useSeed ? (float)Rng.NextDouble(_minValue, _maxValue) : UnityEngine.Random.Range(_minValue, _maxValue);
            Debug.Log(value);
            return value;
        }
    }
}