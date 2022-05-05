using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Utilities
{
    public abstract class AbstractObjectFollow : MonoBehaviour
    {
        [SerializeField] private bool followRotation = true;
        [SerializeField] private Vector3 offset = default;
        public abstract Transform Target { get; }

        protected virtual void Update()
        {
            if (Target == null)
                return;

            transform.position = Target.position + offset;

            if (followRotation)
            {
                transform.rotation = Target.rotation;
            }
        }
    }
}