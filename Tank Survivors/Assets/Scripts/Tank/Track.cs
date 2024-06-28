using DG.Tweening;
using UnityEngine;

namespace Tank
{
    [AddComponentMenu("Scripts/Tank/Tank.Track")]
    internal class Track : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private float fadeDuration;

        private Tween fadeTween;

        public void Fade()
        {
            fadeTween = spriteRenderer
                .material.DOFade(0, fadeDuration)
                .OnComplete(() => Destroy(gameObject));
        }

        private void OnDestroy()
        {
            fadeTween?.Kill();
        }
    }
}
