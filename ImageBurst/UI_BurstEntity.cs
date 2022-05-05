using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace D_Utilities
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
        }
    }
}