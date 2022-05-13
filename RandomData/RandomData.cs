using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Framework 
{
    public abstract class AbstractRandomData<T>
    {
        [SerializeField] protected bool useSeed = false;
        [SerializeField] private int seed = default;
        [SerializeField] protected T minValue = default;
        [SerializeField] protected T maxValue = default;

        private D_Rng rng;
        protected D_Rng Rng
        {
            get
            {
                if (rng == null)
                {
                    rng = new D_Rng(seed);
                }

                return rng;
            }
        }

        public abstract T GetRandomValue();

        public abstract T GetRandomValue(T _minValue, T _maxValue);
    }
}