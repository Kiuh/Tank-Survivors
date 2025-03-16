using System;
using System.Collections.Generic;
using DG.Tweening;
using General;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Panels.Tutorial
{
    [AddComponentMenu("Scripts/Panels/Tutorial/Panels.Tutorial.Controller")]
    internal class Controller : MonoBehaviour
    {
        [Serializable]
        private struct Description
        {
            [TextArea(10, 100)]
            public string Text;
            public Sprite Sprite;
        }

        [Required]
        [SerializeField]
        private Image graphic;

        [SerializeField]
        private float graphicFadeInTime;

        [SerializeField]
        private float graphicFadeOutTime;

        [Required]
        [SerializeField]
        private Button exitButton;

        [Required]
        [SerializeField]
        private Button continueButton;

        [Required]
        [SerializeField]
        private TMP_Text descriptionLabel;

        [SerializeField]
        private List<Description> descriptions;
        private IEnumerator<Description> enumerator;

        private void Awake()
        {
            exitButton.onClick.AddListener(ExitTutorial);
            continueButton.onClick.AddListener(HandleContinue);
            enumerator = descriptions.GetEnumerator();
        }

        private void Start()
        {
            HandleContinue();
        }

        private Sequence imageShowTween;

        private void HandleContinue()
        {
            if (enumerator.MoveNext())
            {
                imageShowTween?.Kill();
                descriptionLabel.text = enumerator.Current.Text;

                imageShowTween = DOTween.Sequence();
                _ = imageShowTween
                    .Append(
                        graphic
                            .DOFade(0, graphicFadeInTime)
                            .OnComplete(() => graphic.sprite = enumerator.Current.Sprite)
                    )
                    .Append(graphic.DOFade(1, graphicFadeOutTime));
            }
            else
            {
                ExitTutorial();
            }
        }

        private void ExitTutorial()
        {
            ScenesController.Instance.LoadScene(InGameScene.MainScene);
        }

        private void OnDestroy()
        {
            imageShowTween?.Kill();
        }
    }
}
