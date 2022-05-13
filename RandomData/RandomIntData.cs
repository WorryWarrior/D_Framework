using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Framework
{
    [System.Serializable]
    public class RandomIntData : AbstractRandomData<int>
    {
        public override int GetRandomValue()
        {
            int value = useSeed ? Rng.RollRandomIntInRange(minValue, maxValue) : UnityEngine.Random.Range(minValue, maxValue);
            return value;
        }

        public override int GetRandomValue(int _minValue, int _maxValue)
        {
            int value = useSeed ? Rng.RollRandomIntInRange(_minValue, _maxValue) : UnityEngine.Random.Range(_minValue, _maxValue);
            return value;
        }
    }
}
