using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using D_NaughtyAttributes;

namespace D_Utilities.VisualPrototyping
{
    public class VisualPrototyper : MonoBehaviour
    {
        [Expandable]
        [SerializeField] private VisualActionAbstractTrigger visualActionTrigger = null;
        [SerializeField] private List<VisualActionBlock> actionBlocks = null;

        private int triggerPressCount = 0;

        private List<VisualPrototyperSubscriber> subscribers = new List<VisualPrototyperSubscriber>();

        private void Start()
        {
            visualActionTrigger.OnVisualActionTriggered += () =>
            {
                if (triggerPressCount < actionBlocks.Count)
                {
                    actionBlocks[triggerPressCount].ExecuteActionBlock();
                }

                for (int i = 0; i < subscribers.Count; i++)
                {
                    subscribers[i].ExecuteSubscriberActionBlock(triggerPressCount);
                }

                visualActionTrigger.UpdateData(new VisualActionTImerTriggerData(EvaluateBlockDelay(triggerPressCount)));
                triggerPressCount++;
            };

            visualActionTrigger.StartTracking();
        }

        public void Subscribe(VisualPrototyperSubscriber visualPrototyperSubscriber)
        {
            subscribers.Add(visualPrototyperSubscriber);
        }

        private float EvaluateBlockDelay(int index)
        {
            float blockDelay;
            float maxDelay = 0f;

            if (index < actionBlocks.Count)
            {
                maxDelay = actionBlocks[index].MaxBlockDelay;
            }

            for (int i = 0; i < subscribers.Count; i++)
            {
                blockDelay = subscribers[i].GetMaxBlockDelay(index);
                if (blockDelay > maxDelay)
                    maxDelay = blockDelay;
                
            }

            return maxDelay;
        }
    }
}