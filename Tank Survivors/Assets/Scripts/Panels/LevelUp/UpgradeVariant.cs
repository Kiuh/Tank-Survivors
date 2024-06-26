using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Panels.LevelUp
{
    [AddComponentMenu("Panels.LevelUp.UpgradeVariant")]
    public class UpgradeVariant : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private Button button;
        public Button Button => button;

        [Required]
        [SerializeField]
        private TMP_Text informationLabel;
        public string InformationText
        {
            get => informationLabel.text;
            set => informationLabel.text = value;
        }
    }
}
