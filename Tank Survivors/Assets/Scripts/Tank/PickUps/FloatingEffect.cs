using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Tank.PickUps
{
    [AddComponentMenu("Scripts/Tank/PickUps/Tank.PickUps.FloatingEffect")]
    internal class FloatingEffect : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private TMP_Text label;

        [SerializeField]
        private float duration;

        [SerializeField]
        private float length;

        private Tween moveTween;
        private Tween fadeTween;

        public void CreateAndLaunch(Vector3 position)
        {
            FloatingEffect newInstance = Instantiate(this, position, Quaternion.identity);
            newInstance.Launch();
        }

        public void CreateAndLaunch(Vector3 position, string text, Color color)
        {
            FloatingEffect newInstance = Instantiate(this, position, Quaternion.identity);
            newInstance.Launch(text, color);
        }

        public void Launch(string text, Color color)
        {
            label.text = text;
            label.color = color;
            Launch();
        }

        private void Launch()
        {
            moveTween = transform.DOLocalMoveY(transform.position.y + length, duration);
            fadeTween = label.DOFade(0, duration).OnComplete(() => Destroy(gameObject));
        }

        private void OnDestroy()
        {
            moveTween?.Kill();
            fadeTween?.Kill();
        }
    }
}
