using DG.Tweening;
using General;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Panels
{
    [AddComponentMenu("Scripts/Panels/Boss/Panels.Boss.Warning")]
    internal class Warning : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private EnemyGenerator enemyGenerator;

        [Required]
        [SerializeField]
        private Image image;

        [SerializeField]
        private float scaleDown;

        [SerializeField]
        private float scaleUp;

        [SerializeField]
        private float scaleTime;

        [SerializeField]
        private int pulseCount;

        [SerializeField]
        private AnimationCurve inCurve;

        [SerializeField]
        private AnimationCurve outCurve;

        private Sequence bossSequence;

        public UnityEvent BossWarningShowed;

        private void Awake()
        {
            enemyGenerator.OnBossAppeared += (e) => ShowBossWarning();
        }

        public void ShowBossWarning()
        {
            BossWarningShowed?.Invoke();
            bossSequence?.Kill();

            bossSequence = DOTween.Sequence();
            _ = bossSequence.OnStart(() =>
            {
                image.gameObject.SetActive(true);
                image.transform.localScale = Vector3.one * scaleDown;
            });

            _ = bossSequence.Append(
                image.transform.DOScale(Vector3.one * scaleUp, scaleTime).SetEase(inCurve)
            );
            _ = bossSequence.Join(image.DOFade(1, scaleTime));
            _ = bossSequence.Append(
                image.transform.DOScale(Vector3.one * scaleDown, scaleTime).SetEase(outCurve)
            );

            for (int i = 0; i < pulseCount - 1; i++)
            {
                _ = bossSequence.Append(
                    image.transform.DOScale(Vector3.one * scaleUp, scaleTime).SetEase(inCurve)
                );
                _ = bossSequence.Append(
                    image.transform.DOScale(Vector3.one * scaleDown, scaleTime).SetEase(outCurve)
                );
            }
            _ = bossSequence.Join(image.DOFade(0, scaleTime));
            _ = bossSequence.OnComplete(() => image.gameObject.SetActive(false));
            _ = bossSequence.Play();
        }

        private void OnDestroy()
        {
            bossSequence?.Kill();
        }
    }
}
