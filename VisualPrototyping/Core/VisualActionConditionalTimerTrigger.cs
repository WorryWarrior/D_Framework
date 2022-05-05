using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

namespace D_Utilities.VisualPrototyping
{
    [CreateAssetMenu(fileName = "New Visual Action Conditional Timer Trigger", menuName = "Custom Objects/Visual Prototyping/Conditional Timer Trigger")]
    public class VisualActionConditionalTimerTrigger : VisualActionAbstractTrigger
    {
        public override void Dispose()
        {
            
        }

        public override void StartTracking()
        {
            Debug.Log(5);
        }

        public override void UpdateData(VisualActionTriggerData data) { }
    }
}