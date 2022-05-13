using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace D_Framework
{
    public class UI_ProgressBarController : MonoBehaviour
    {
        [SerializeField] private Image fillImage = null;
        [SerializeField] private float fillSpeed = 1f;

        private float targetProgress = 0f;

        public void SetFill(float completion, bool forceFill = false)
        {
            targetProgress = completion;

            if (forceFill)
            {
                fillImage.fillAmount = completion;
            }
        }

        private void Update()
        {
            fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, targetProgress, fillSpeed * Time.deltaTime);
        }
    }
}