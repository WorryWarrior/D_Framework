using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Framework.Storages
{
    public abstract class StorageBase<T> : ScriptableObject where T : class
    {
        [SerializeField] private List<T> elements = null;

        protected int ElementCount { get => elements.Count; }
        public abstract T GetElement();

        protected T GetElementInternal(int i)
        {
            if (i < 0 || i >= elements.Count)
            {
                Debug.LogError("Attempted to retrieve non-existent list element");
                return default;
            }

            return elements[i];
        }
    }
}