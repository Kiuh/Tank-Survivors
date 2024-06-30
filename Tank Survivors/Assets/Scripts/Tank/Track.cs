using DG.Tweening;
using Module.ObjectPool;
using UnityEngine;

namespace Tank
{
    [AddComponentMenu("Scripts/Tank/Tank.Track")]
    internal class Track : MonoBehaviour, IPooledObject<Track>
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private float fadeDuration;

        private Tween fadeTween;

        private IPool<Track> pool;

        public void Fade()
        {
            fadeTween = spriteRenderer.material.DOFade(0, fadeDuration).OnComplete(() => Release());
        }

        public void OnGet(IPool<Track> pool)
        {
            this.pool = pool;
        }

        public void OnRelease() { }

        public void Release()
        {
            if (pool != null)
            {
                pool.Release(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            fadeTween?.Kill();
        }
    }
}
