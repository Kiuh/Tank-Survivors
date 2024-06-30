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

        private float startAlfa;
        private Tween fadeTween;
        private IPool<Track> pool;

        private void Start()
        {
            startAlfa = spriteRenderer.material.color.a;
        }

        public void Fade()
        {
            fadeTween = spriteRenderer.material.DOFade(0, fadeDuration).OnComplete(() => Release());
        }

        public void OnGet(IPool<Track> pool)
        {
            this.pool = pool;
        }

        public void OnRelease()
        {
            Color color = spriteRenderer.material.color;
            spriteRenderer.material.color = new Color(color.r, color.g, color.b, startAlfa);
        }

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
