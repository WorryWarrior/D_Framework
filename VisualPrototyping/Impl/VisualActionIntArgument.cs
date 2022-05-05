using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Utilities.VisualPrototyping
{
    //[CreateAssetMenu(fileName = "New Int Argument", menuName = "Custom Objects/Visual Prototyping/Int Argument")]
    public class VisualActionIntArgument : VisualActionArgument
    {
        [SerializeField] private int intValue = default;

        public override object GetArgumentValue()
        {
            return intValue;
        }
    }
}