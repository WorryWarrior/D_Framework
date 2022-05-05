using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Utilities
{
    [System.Serializable]
    public class DOTweenTransitionData<T>
    {
        public T startValue;
        public T endValue;
        public float transitionDuration;
    }
}