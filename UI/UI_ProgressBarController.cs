using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace D_Framework
{
    public class UI_ProgressBarController : MonoBehaviour
    {
        private const float FILL_PRECISION_DELTA = 0.005f;

        [SerializeField] private Image fillImage = null;
        [SerializeField] private float fillSpeed = 1f;

        private float targetProgress = 0f;
        private Coroutine fillRoutine;

        public void SetFillRate(float completion, bool forceFill = false)
        {
            targetProgress = completion;

            if (forceFill)
            {
                fillImage.fillAmount = completion;
            }
        }

        public void Fill(bool overTime, float duration = 1f, EasingFunctionType easingFunctionType = EasingFunctionType.Linear)
        {
            if (overTime)
            {
                StartFillOverTimeRoutine(duration, easingFunctionType);
            }
            else
            {
                StartFillRoutine();
            }
        }

        private IEnumerator FillRoutine(float _targetProgress)
        {
            while (Mathf.Abs(_targetProgress - fillImage.fillAmount) > FILL_PRECISION_DELTA)
            {
                fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, _targetProgress, fillSpeed * Time.deltaTime);
                yield return null;
            }
        }

        private void StartFillRoutine()
        {
            if (fillRoutine != null)
            {
                StopCoroutine(fillRoutine);
            }

            fillRoutine = StartCoroutine(FillRoutine(targetProgress));
        }

        private void StartFillOverTimeRoutine(float duration, EasingFunctionType easingFunctionType)
        {
            if (fillRoutine != null)
            {
                StopCoroutine(fillRoutine);
            }

            fillRoutine = StartCoroutine(FillOverTimeRoutine(targetProgress, duration, easingFunctionType));
        }

        private IEnumerator FillOverTimeRoutine(float _targetProgress, float _duration, EasingFunctionType _easingFunctionType)
        {
            float t = 0f;
            float startProgress = fillImage.fillAmount;

            while (t < _duration)
            {
                float progress = t / _duration;
                EvaluateProgress(_easingFunctionType, ref progress);
                fillImage.fillAmount = Mathf.Lerp(startProgress, _targetProgress, progress);
                t += Time.deltaTime;
                yield return null;
            }

            fillImage.fillAmount = _targetProgress;
        }

        private void EvaluateProgress(EasingFunctionType tweeningFunctionType, ref float progress)
        {
            switch (tweeningFunctionType)
            {
                case EasingFunctionType.Linear:
                    break;
                case EasingFunctionType.Quadratic:
                    progress = progress * progress;
                    break;
                case EasingFunctionType.Qubic:
                    progress = progress * progress * progress;
                    break;
                default:
                    break;
            }
        }

        public enum EasingFunctionType
        {
            Linear = 0,
            Quadratic = 1,
            Qubic = 2,
        }
    }
}