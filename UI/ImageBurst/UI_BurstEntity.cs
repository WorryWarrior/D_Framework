using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace D_Framework
{
    public class UI_BurstEntity : MonoBehaviour
    {
        private RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public void MoveToTarget(Vector2 target, float duration)
        {
            rectTransform.DOAnchorPos(target, duration);
            //StartCoroutine(MoveToTargetRoutine(target, duration));
        }

        private IEnumerator MoveToTargetRoutine(Vector2 target, float duration)
        {
            float t = 0f;
            Vector2 startPosition = rectTransform.anchoredPosition;

            while (t < duration)
            {
                Debug.Log(t / duration);
                rectTransform.anchoredPosition = Vector2.Lerp(startPosition, target, t / duration);
                t += Time.deltaTime;
                yield return null;
            }
        }
    }
}