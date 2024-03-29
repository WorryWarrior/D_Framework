using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Framework
{
    public abstract class PoolingSingleton<T> : PoolingObjectBase where T : Component
    {
        private static bool isDestroyed = false;

        private static T instance;
        public static T Instance
        {
            get
            {
                if (isDestroyed)
                {
                    return null;
                }

                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                }

                if (instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name);
                    instance = obj.AddComponent<T>();
                }

                return instance;
            }
        }

        protected override void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            base.Awake();
        }

        protected virtual void OnDisable()
        {
            isDestroyed = true;
        }
    }
}