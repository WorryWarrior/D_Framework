using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Utilities.VisualPrototyping
{
    public class VisualActionTImerTriggerData : VisualActionTriggerData
    {
        public float blockDelay;

        public VisualActionTImerTriggerData(float _blockDelay)
        {
            blockDelay = _blockDelay;
        }
    }
}