using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Framework
{
    public class ObjectFollow : AbstractObjectFollow
    {
        [SerializeField] private Transform target = null;

        public override Transform Target
        {
            get => target;
        }

        public void SetTarget(Transform _target)
        {
            target = _target;
        }
    }
}