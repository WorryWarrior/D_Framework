using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Utilities.VisualPrototyping
{
    public abstract class VisualActionArgument : ScriptableObject
    {
        public abstract object GetArgumentValue();
    }
}