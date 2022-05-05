using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Utilities.VisualPrototyping
{
    [CreateAssetMenu(fileName = "New Visual Action Timer Trigger", menuName = "Custom Objects/Visual Prototyping/Timer Trigger")]
    public class VisualActionTimerTrigger : VisualActionAbstractTrigger
    {
        [SerializeField] private float tickDuration = 1f;

        private float blockDelay;
        private Coroutine trackRoutine;

        public override void StartTracking()
        {
            trackRoutine = MB_Utilities.StartHostCoroutine(TickRoutine());
        }

        public override void Dispose()
        {
            if (MB_Utilities.Instance == null)
                return;

            MB_Utilities.StopHostCoroutine(trackRoutine);
        }

        public override void UpdateData(VisualActionTriggerData data)
        {
            VisualActionTImerTriggerData timerTriggerData = data as VisualActionTImerTriggerData;
            blockDelay = timerTriggerData.blockDelay;
        }

        private IEnumerator TickRoutine()
        {
            yield return new WaitForSeconds(blockDelay + tickDuration);
            InvokeVisualActionEvent();

            yield return TickRoutine();
        }
    }
}