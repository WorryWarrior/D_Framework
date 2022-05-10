using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Utilities.VisualPrototyping
{
    public class VisualActionFloatArgument : VisualActionArgument
    {
        [SerializeField] private float flaotValue = default;

        public override object GetArgumentValue()
        {
            return flaotValue;
        }
    }
}
