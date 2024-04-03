using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Panels.Levels
{
    [AddComponentMenu("Panels.Levels.Level")]
    public class Level : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI levelName;

        [SerializeField]
        private Button button;

        public void SetupLevelButton(string levelName, Action action)
        {
            this.levelName.text = levelName;
            button.onClick.AddListener(() => action());
        }
    }
}
