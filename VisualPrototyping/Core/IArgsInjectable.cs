using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Utilities.VisualPrototyping
{
    public interface IArgsInjectable
    {
        public void InjectArgs(VisualActionArgs args);
    }
}