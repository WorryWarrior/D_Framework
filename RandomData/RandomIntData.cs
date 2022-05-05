using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Utilities
{
    [System.Serializable]
    public class RandomIntData : AbstractRandomData<int>
    {
        public override int GetRandomValue()
        {
            int value = useSeed ? Rng.Next(minValue, maxValue) : UnityEngine.Random.Range(minValue, maxValue);
            return value;
        }

        public override int GetRandomValue(int _minValue, int _maxValue)
        {
            int value = useSeed ? Rng.Next(_minValue, _maxValue) : UnityEngine.Random.Range(_minValue, _maxValue);
            return value;
        }
    }
}
