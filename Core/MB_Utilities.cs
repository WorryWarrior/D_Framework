using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Utilities
{
    public class MB_Utilities : Singleton<MB_Utilities>
    {
        private WaitForEndOfFrame frameDelay = null;
        private WaitForEndOfFrame FrameDelay
        {
            get
            {
                if (frameDelay == null)
                {
                    frameDelay = new WaitForEndOfFrame();
                }

                return frameDelay;
            }
        }

        public static Coroutine StartHostCoroutine(IEnumerator routine)
        {
            return Instance.StartCoroutine(routine);
        }

        public static void StopHostCoroutine(Coroutine coroutine)
        {
            Instance.StopCoroutine(coroutine);
        }

        public static Coroutine ExecuteWhen(System.Func<bool> condition, System.Action action)
        {
            return Instance.StartCoroutine(Instance.ExecuteWhenRoutine(condition, action));
        }

        private int emergency = 0;

        private IEnumerator ExecuteWhenRoutine(System.Func<bool> condition, System.Action action)
        {
            emergency++;

            if (emergency >= 25)
            {
                Debug.LogError("Emergency");
                yield break;
            }


            while (!condition())
            {
                yield return null;
            }

            action();

            yield return FrameDelay;
            yield return ExecuteWhenRoutine(condition, action);
        }

        public static Coroutine Execute(float delay, System.Action coroutineAction)
        {
            return Instance.StartCoroutine(Instance.ExecuteRoutine(delay, coroutineAction));
        }

        private IEnumerator ExecuteRoutine(float delay, System.Action coroutineAction)
        {
            yield return new WaitForSeconds(delay);
            coroutineAction();
        }
    }
}