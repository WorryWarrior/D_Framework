using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Utilities.VisualPrototyping
{
    //[CreateAssetMenu(fileName = "New Bool Argument", menuName = "Custom Objects/Visual Prototyping/Bool Argument")]
    public class VisualActionBoolArgument : VisualActionArgument
    {
        [SerializeField] private bool boolValue = default;

        public override object GetArgumentValue()
        {
            return boolValue;
        }
    }
}
