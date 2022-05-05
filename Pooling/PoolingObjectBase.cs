using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Utilities
{
    public abstract class PoolingObjectBase : MonoBehaviour
    {
        protected List<List<GameObject>> pooledObjectsList = new List<List<GameObject>>();
        protected List<int> positions = new List<int>();

        protected virtual void Awake()
        {
            InitializePool();
        }

        protected abstract void InitializePool();

        protected virtual GameObject GetPooledObject(int index)
        {
            int poolSize = pooledObjectsList[index].Count;
            for (int i = positions[index] + 1; i < positions[index] + pooledObjectsList[index].Count; i++)
            {
                if (!pooledObjectsList[index][i % poolSize].activeInHierarchy)
                {
                    positions[index] = i % poolSize;
                    pooledObjectsList[index][i % poolSize].SetActive(true);
                    return pooledObjectsList[index][i % poolSize];
                }
            }

            return null;
        }

        protected virtual void ResetElements(int index)
        {
            for (int i = 0; i < pooledObjectsList[index].Count; i++)
            {
                pooledObjectsList[index][i].SetActive(false);
            }
        }

        protected virtual List<GameObject> GetAllPooledObjects(int index)
        {
            return pooledObjectsList[index];
        }

        [System.Serializable]
        protected class ObjectPoolItemBase
        {
            public GameObject pooledObject;
            public int pooledAmount;
        }
    }
}