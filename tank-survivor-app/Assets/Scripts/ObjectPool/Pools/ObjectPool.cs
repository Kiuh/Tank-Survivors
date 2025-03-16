using System;
using System.Collections.Generic;
using UnityEngine;

namespace Module.ObjectPool
{
    public class ObjectPool<T> : IPool<T>
    {
        private Func<T> preloadFunc;
        private Action<T> getAction;
        private Action<T> releaseAction;

        private Queue<T> pool = new();
        private List<T> active = new();

        public IReadOnlyCollection<T> Active => active;

        public ObjectPool(
            Func<T> preloadFunc,
            Action<T> getAction,
            Action<T> releaseAction,
            int preloadCount
        )
        {
            this.preloadFunc = preloadFunc;
            this.getAction = getAction;
            this.releaseAction = releaseAction;

            if (this.preloadFunc == null)
            {
                Debug.LogError("There is no preloadFunc");
            }

            for (int i = 0; i < preloadCount; i++)
            {
                Release(this.preloadFunc.Invoke());
            }
        }

        public object GetPreloadFunc()
        {
            return preloadFunc;
        }

        public T Get()
        {
            if (pool.Count == 0)
            {
                pool.Enqueue(preloadFunc.Invoke());
            }

            T pooledObject = pool.Dequeue();
            active.Add(pooledObject);

            if (pooledObject is IPooledObject<T> poolableObject)
            {
                poolableObject.OnGet(this);
            }

            getAction?.Invoke(pooledObject);
            return pooledObject;
        }

        public void Release(T obj)
        {
            _ = active.Remove(obj);
            pool.Enqueue(obj);

            if (obj is IPooledObject<T> poolableObject)
            {
                poolableObject.OnRelease();
            }

            releaseAction?.Invoke(obj);
        }
    }
}
