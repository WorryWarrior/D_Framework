using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Utilities.VisualPrototyping
{
    public class VisualActionArgumentFactory : IFactory<VisualActionArgument>
    {
        public VisualActionArgument GetElement(Enum visualArgumentType)
        {
            VisualActionArgType castedArgumentType = (VisualActionArgType)visualArgumentType;
            return ScriptableObject.CreateInstance(castedArgumentType.GetLinkedArgumentType()) as VisualActionArgument;

            //return castedArgumentType switch
            //{

            /* VisualActionArgType.Int => ScriptableObject.CreateInstance<VisualActionIntArgument>(),
             VisualActionArgType.Float => ScriptableObject.CreateInstance<VisualActionFloatArgument>(),
             VisualActionArgType.Bool => ScriptableObject.CreateInstance<VisualActionBoolArgument>(),
             VisualActionArgType.String => ScriptableObject.CreateInstance<VisualActionStringArgument>(),
             _ => null,*/
            //};
        }

    }
}