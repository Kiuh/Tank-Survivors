using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Panels.LevelUp
{
    [AddComponentMenu("Panels.LevelUp.UpgradeVariant")]
    public class UpgradeVariant : MonoBehaviour
    {
        [SerializeField]
        private Button button;
        public Button Button => button;

        [SerializeField]
        private TMP_Text informationLabel;
        public string InformationText
        {
            get => informationLabel.text;
            set => informationLabel.text = value;
        }
    }
}
