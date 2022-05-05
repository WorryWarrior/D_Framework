using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Utilities.VisualPrototyping
{
    public enum VisualActionArgType
    {
        [LinkedVisualActionArgumentType(typeof(VisualActionIntArgument))]
        Int,

        [LinkedVisualActionArgumentType(typeof(VisualActionFloatArgument))]
        Float,

        [LinkedVisualActionArgumentType(typeof(VisualActionBoolArgument))]
        Bool,

        [LinkedVisualActionArgumentType(typeof(VisualActionStringArgument))]
        String
    }
}