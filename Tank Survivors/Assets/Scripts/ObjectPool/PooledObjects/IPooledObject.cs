namespace Module.ObjectPool
{
    public interface IPooledObject<T>
    {
        public void OnGet(IPool<T> pool);
        public void Release();
        public void OnRelease();
    }
}
