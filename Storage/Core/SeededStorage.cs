using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Utilities.Storages
{
    public abstract class SeededStorage<T> : StorageBase<T> where T : class //UnityEngine.Object
    {
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

            return GetElementInternal(rng.Next(0, ElementCount));
        }
    }
}