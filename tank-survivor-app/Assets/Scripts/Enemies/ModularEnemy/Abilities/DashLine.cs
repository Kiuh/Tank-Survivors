using Sirenix.OdinInspector;
using UnityEngine;

namespace Enemies.ModularEnemy.Abilities
{
    [AddComponentMenu(
        "Scripts/Enemies/ModularEnemy/Abilities/Enemies.ModularEnemy.Abilities.DashLine"
    )]
    internal class DashLine : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        public void RefreshLine()
        {
            SetLength(0);
            SetAlpha(0);
        }

        public void SetLength(float value)
        {
            Vector3 ls = transform.localScale;
            transform.localScale = new Vector3(ls.x, value, ls.z);
        }

        public void SetAlpha(float value)
        {
            Color col = spriteRenderer.color;
            spriteRenderer.color = new Color(col.r, col.g, col.b, value);
        }
    }
}
