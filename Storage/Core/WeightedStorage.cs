using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Utilities.Storages
{
    public abstract class WeightedStorage<T> : StorageBase<T> where T : class
    {
        [SerializeField] private List<float> percentages = new List<float>();
        private System.Random rng;

        public void InitializeData(int seed)
        {
            rng = new System.Random(seed);
        }

        public override T GetElement()
        {
            if (rng == null)
            {
                Debug.LogError($"Storage {name} was not initialized ");
                return default;
            }

            double percentage = rng.NextDouble() * 100f;

            for (int i = 0; i < ElementCount; i++)
            {
                if (percentage <= percentages[i])
                {
                    return GetElementInternal(i);
                }
                else
                {
                    percentage -= percentages[i];
                }
            }

            Debug.LogError("Unexpected element returned");
            return default;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (ElementCount - percentages.Count > 0)
            {
                for (int i = 0; i < ElementCount - percentages.Count; i++)
                {
                    percentages.Add(default);
                }
            }
            else
            {
                for (int i = percentages.Count - 1; i >= ElementCount; i--)
                {
                    percentages.RemoveAt(i);
                }
            }
        }
#endif
    }

}