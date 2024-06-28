using DG.Tweening;
using UnityEngine;

namespace Enemies
{
    [AddComponentMenu("Scripts/Enemies/Enemies.BlinkSprite")]
    internal class BlinkSprite : MonoBehaviour
    {
        [SerializeField]
        private Color firstColor;

        [SerializeField]
        private Color secondColor;

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private float interval;

        private Tween colorTween;

        private void Start()
        {
            ToFirstColor();
        }

        private void ToFirstColor()
        {
            colorTween = spriteRenderer
                .material.DOColor(firstColor, interval)
                .OnComplete(ToSecondColor);
        }

        private void ToSecondColor()
        {
            colorTween = spriteRenderer
                .material.DOColor(secondColor, interval)
                .OnComplete(ToFirstColor);
        }

        private void OnDestroy()
        {
            colorTween?.Kill();
        }
    }
}
