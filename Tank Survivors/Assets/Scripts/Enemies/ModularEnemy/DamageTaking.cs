using DG.Tweening;
using UnityEngine;

namespace Enemies.ModularEnemy
{
    [AddComponentMenu("Scripts/Enemies/ModularEnemy/Enemies.ModularEnemy.DamageTaking")]
    internal class DamageTaking : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private Color normalColor;

        [SerializeField]
        private Color damageColor;

        [SerializeField]
        private float damageTime;

        private Tween damageTween;

        public void TakeDamage()
        {
            damageTween?.Kill();
            spriteRenderer.material.color = damageColor;
            damageTween = spriteRenderer.material.DOColor(normalColor, damageTime);
        }

        private void OnDestroy()
        {
            damageTween?.Kill();
        }
    }
}
