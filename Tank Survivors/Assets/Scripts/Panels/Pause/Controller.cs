using System.Linq;
using DG.Tweening;
using General;
using Sirenix.OdinInspector;
using Tank;
using UnityEngine;
using UnityEngine.UI;

namespace Panels.Pause
{
    [AddComponentMenu("Panels.Pause.Controller")]
    public class Controller : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private View view;

        [RequiredIn(PrefabKind.PrefabInstanceAndNonPrefabInstance)]
        [SerializeField]
        private GlobalInput globalInput;

        [SerializeField]
        private float resumeDelay;

        [SerializeField]
        private float startAlfa;

        [Required]
        [SerializeField]
        private Image lerpImage;

        [RequiredIn(PrefabKind.PrefabInstanceAndNonPrefabInstance)]
        [SerializeField]
        private TankImpl tankImpl;

        [Required]
        [SerializeField]
        private StatGrid attributesStatGrid;

        [Required]
        [SerializeField]
        private StatGrid weaponStatGrid;

        public void HidePause()
        {
            view.HidePausePanel();
        }

        public void ShowPause()
        {
            view.ShowPausePanel();
            tankImpl.StopScreenEffects();
            attributesStatGrid.FillWithData(tankImpl.GetStatBlockData());
            weaponStatGrid.FillWithData(tankImpl.Weapons.First().GetStatBlockData());
            (transform as RectTransform).ForceUpdateRectTransforms();
        }

        private Tween delayTween;

        public void Resume()
        {
            view.HidePausePanel();

            lerpImage.gameObject.SetActive(true);
            lerpImage.color = new Color(
                lerpImage.color.r,
                lerpImage.color.g,
                lerpImage.color.b,
                startAlfa
            );
            delayTween = lerpImage
                .DOFade(0, resumeDelay)
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    globalInput.UnSetPause();
                    lerpImage.gameObject.SetActive(false);
                });
        }

        public void Repeat()
        {
            globalInput.UnSetPause();
            ScenesController.Instance.LoadScene(InGameScene.GameplayScene);
        }

        public void Leave()
        {
            globalInput.UnSetPause();
            ScenesController.Instance.LoadScene(InGameScene.MainScene);
        }

        private void OnDestroy()
        {
            delayTween?.Kill();
        }
    }
}
