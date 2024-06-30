using System.Collections.Generic;
using UnityEngine;

namespace Module.ObjectPool
{
    public class MonoBehObjectPool<T> : IPool<T>
        where T : MonoBehaviour
    {
        private ObjectPool<T> core;

        public IReadOnlyCollection<T> Active => core.Active;

        public MonoBehObjectPool(T objectTemplate, int startCount, Transform parent = null)
        {
            core = new(
                () =>
                {
                    T pooledObject = GameObject.Instantiate(objectTemplate, parent);
                    pooledObject.gameObject.SetActive(false);
                    return pooledObject;
                },
                (obj) => obj.gameObject.SetActive(true),
                (obj) =>
                {
                    obj.gameObject.SetActive(false);
                    if (parent != null)
                    {
                        obj.transform.SetParent(parent);
                    }
                },
                startCount
            );
        }

        public T Get()
        {
            T pooledObject = core.Get();
            return pooledObject;
        }

        public void Release(T pooledObject)
        {
            core.Release(pooledObject);
        }
    }
}
