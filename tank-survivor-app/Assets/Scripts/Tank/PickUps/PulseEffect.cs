using DG.Tweening;
using UnityEngine;

namespace Tank.PickUps
{
    [AddComponentMenu("Scripts/Tank/PickUps/Tank.PickUps.PulseEffect")]
    internal class PulseEffect : MonoBehaviour
    {
        [SerializeField]
        private float minScale;

        [SerializeField]
        private float maxScale;

        [SerializeField]
        private float interval;

        [SerializeField]
        private float intervalBias;

        private Tween scaleTween;

        private void Start()
        {
            ScaleUp();
        }

        private void ScaleUp()
        {
            scaleTween = transform.DOScale(maxScale, GetInterval()).OnComplete(ScaleDown);
        }

        private void ScaleDown()
        {
            scaleTween = transform.DOScale(minScale, GetInterval()).OnComplete(ScaleUp);
        }

        private float GetInterval()
        {
            return interval + Random.Range(-intervalBias, intervalBias);
        }

        private void OnDestroy()
        {
            scaleTween?.Kill();
        }
    }
}
