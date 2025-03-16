using UnityEngine;

namespace Module.ObjectPool
{
    public class PooledParticle : MonoBehaviour, IPooledObject<PooledParticle>
    {
        [SerializeField]
        private ParticleSystem particle;

        private IPool<PooledParticle> pool;

        public ParticleSystem Particle => particle;
        public float SizeMultiplier { get; private set; } = 1;
        private ParticleSystem.MainModule ParticleMain => particle.main;

        private void OnParticleSystemStopped()
        {
            Release();
        }

        public void ChangeColor(Color color)
        {
            ParticleSystem.MainModule reserveMain = ParticleMain;
            reserveMain.startColor = color;
        }

        public void Resize(float multiplier)
        {
            SizeMultiplier = multiplier;
            transform.localScale *= SizeMultiplier;
        }

        public void OnGet(IPool<PooledParticle> pool)
        {
            this.pool = pool;
        }

        public void OnRelease() { }

        public void Release()
        {
            if (pool != null)
            {
                Resize(1 / SizeMultiplier);
                pool.Release(this);
                return;
            }

            Destroy(gameObject);
        }
    }
}
