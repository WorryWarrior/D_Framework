using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Utilities.Storages
{
    public abstract class SeededStorage<T> : StorageBase<T> where T : class //UnityEngine.Object
    {
        private D_Rng rng;

        public void InitializeData(int seed)
        {
            rng = new D_Rng(seed);
        }

        public override T GetElement()
        {
            if (rng == null)
            {
                Debug.LogError($"Storage {name} was not initialized ");
                return default;
            }

            return GetElementInternal(rng.RollRandomIntInRange(0, ElementCount));
        }
    }
}