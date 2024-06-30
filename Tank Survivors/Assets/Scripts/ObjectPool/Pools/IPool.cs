using System.Collections.Generic;

namespace Module.ObjectPool
{
    public interface IPool<T>
    {
        public IReadOnlyCollection<T> Active { get; }

        public T Get();
        public void Release(T obj);
    }
}
