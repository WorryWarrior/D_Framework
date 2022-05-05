using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Utilities
{
    public class UI_BurstController : PoolingObjectBase
    {
        [SerializeField] private RandomFloatData randomData = null;

        [SerializeField] private float transitionTime = 1f;
        [SerializeField] private float transitionTimeOffset = 0f;

        [SerializeField] private List<UI_BurstObjectPoolItem> itemsToPool = new List<UI_BurstObjectPoolItem>();

        public event System.Action OnAllImagesReached;

        [Header("Preview")]
        [SerializeField] private int previewIndex = 0;

        [D_NaughtyAttributes.Button]
        private void PreviewBurst()
        {
            Burst(previewIndex);
        }

        public void Burst(int burstEntityIndex)
        {
            List<GameObject> pooledObjects = GetAllPooledObjects(burstEntityIndex);
            UI_BurstObjectPoolItem burstObjectPoolItem = itemsToPool[burstEntityIndex];

            for (int i = 0; i < pooledObjects.Count; i++)
            {
                pooledObjects[i].With(it =>
                {
                    it.SetActive(true);
                    it.GetComponent<RectTransform>().anchoredPosition = GetImagePosition(burstObjectPoolItem);
                    float reachTime = randomData.GetRandomValue(transitionTime + transitionTimeOffset, transitionTime - transitionTimeOffset);
                    it.GetComponent<UI_BurstEntity>().MoveToTarget(
                        RectTransformUtility.CalculateRelativeRectTransformBounds(burstObjectPoolItem.targetCanvas.transform,
                        burstObjectPoolItem.target.transform).center, reachTime);

                    CoroutineActions.ExecuteAction(reachTime, () =>
                        it.SetActive(false)
                    );
                });
            }

            CoroutineActions.ExecuteAction(transitionTime + transitionTimeOffset, () =>
                        OnAllImagesReached?.Invoke()
                    );
        }

        private Vector3 GetImagePosition(UI_BurstObjectPoolItem burstObjectPoolItem)
        {
            Vector2 anchoredPosition = burstObjectPoolItem.originBounds.anchoredPosition;
            Vector2 dimensions = new Vector2(burstObjectPoolItem.originBounds.rect.width, burstObjectPoolItem.originBounds.rect.height);

            Vector3 res = new Vector3(anchoredPosition.x + randomData.GetRandomValue(-dimensions.x * 0.5f, dimensions.x * 0.5f),
                anchoredPosition.y + randomData.GetRandomValue(-dimensions.y * 0.5f, dimensions.y * 0.5f), 0);

            return res;
        }

        protected override void InitializePool()
        {
            for (int i = 0; i < itemsToPool.Count; i++)
            {
                UI_BurstObjectPoolItem item = itemsToPool[i];
                List<GameObject> pooledObjects = new List<GameObject>();

                for (int j = 0; j < item.pooledAmount; j++)
                {
                    GameObject obj = Instantiate(item.pooledObject, transform);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                }

                pooledObjectsList.Add(pooledObjects);
                positions.Add(0);
            }
        }

        [System.Serializable]
        private class UI_BurstObjectPoolItem : ObjectPoolItemBase
        {
            public RectTransform target;
            public RectTransform originBounds;
            public Canvas targetCanvas;
        }
    }
}