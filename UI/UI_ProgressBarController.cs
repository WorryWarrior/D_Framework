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

        public void SetFill(float completion, bool forceFill = false)
        {
            targetProgress = completion;

            if (forceFill)
            {
                fillImage.fillAmount = completion;
            }

            StartFillRoutine();
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
    }
}