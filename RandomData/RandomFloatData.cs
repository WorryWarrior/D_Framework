using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Framework
{
    [System.Serializable]
    public class RandomFloatData : AbstractRandomData<float>
    {
        public override float GetRandomValue()
        {
            float value = useSeed ? Rng.RollRandomFloatInRange(minValue, maxValue) : UnityEngine.Random.Range(minValue, maxValue);
            return value;
        }

        public override float GetRandomValue(float _minValue, float _maxValue)
        {
            float value = useSeed ? Rng.RollRandomFloatInRange(_minValue, _maxValue) : UnityEngine.Random.Range(_minValue, _maxValue);
            return value;
        }
    }
}