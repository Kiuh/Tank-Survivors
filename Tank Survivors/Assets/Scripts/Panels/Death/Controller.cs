﻿using System.Text;
using Audio;
using General;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Panels.Death
{
    [AddComponentMenu("Panels.Death.Controller")]
    public class Controller : MonoBehaviour
    {
        [RequiredIn(PrefabKind.PrefabInstanceAndNonPrefabInstance)]
        [SerializeField]
        private ProgressController progressController;

        [RequiredIn(PrefabKind.PrefabInstanceAndNonPrefabInstance)]
        [SerializeField]
        private General.Timer timer;

        [Required]
        [SerializeField]
        private View view;

        public UnityEvent<int> OnUseSecondLife;

        private void Awake()
        {
            progressController.OnLoose += ShowLosePanel;
        }

        public void ShowLosePanel()
        {
            Time.timeScale = 0.0f;
            SoundsManager.Instance.PauseSounds();
            view.ShowLosePanel(GetInfoString(), progressController.Progress);
        }

        public void HideLosePanel()
        {
            SoundsManager.Instance.UnPauseSounds();
            Time.timeScale = 1.0f;
            view.HideLosePanel();
        }

        public void HideSecondLifeButton()
        {
            view.HideSecondLifeButton();
        }

        public void UseSecondLifeBonus()
        {
            OnUseSecondLife?.Invoke((int)Reward.SecondLife);
        }

        public void RepeatGame()
        {
            ScenesController.Instance.LoadScene(InGameScene.GameplayScene);
        }

        public void LeaveGame()
        {
            ScenesController.Instance.LoadScene(InGameScene.MainScene);
        }

        private string GetInfoString()
        {
            StringBuilder stringBuilder = new();
            _ = stringBuilder.AppendLine($"Время: {timer.FormattedCurrentTime}");
            return stringBuilder.ToString();
        }
    }
}
