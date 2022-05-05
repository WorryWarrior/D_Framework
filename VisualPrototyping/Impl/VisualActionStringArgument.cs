using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Utilities.VisualPrototyping
{
    //[CreateAssetMenu(fileName = "New Int Argument", menuName = "Custom Objects/Visual Prototyping/String Argument")]
    public class VisualActionStringArgument : VisualActionArgument
    {
        [SerializeField] private string stringValue = default;

        public override object GetArgumentValue()
        {
            return stringValue;
        }
    }
}
