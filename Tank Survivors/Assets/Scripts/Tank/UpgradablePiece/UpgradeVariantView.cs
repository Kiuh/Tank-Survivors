using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tank.UpgradablePiece
{
    [AddComponentMenu("Tank.UpgradablePiece.UpgradeVariantView")]
    public class UpgradeVariantView : MonoBehaviour
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
