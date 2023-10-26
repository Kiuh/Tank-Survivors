using TMPro;
using UnityEngine;

namespace Tank.UpgradablePiece
{
    [AddComponentMenu("Tank.UpgradablePiece.UpgradeBlockView")]
    public class UpgradeBlockView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text upgradeNameLabel;
        public string UpgradeName
        {
            get => upgradeNameLabel.text;
            set => upgradeNameLabel.text = value;
        }

        [SerializeField]
        private UpgradeVariantView upgradeVariantViewPrefab;

        [SerializeField]
        private Transform upgradeVariantViewRoot;

        public UpgradeVariantView CreateUpgradeVariantView()
        {
            return Instantiate(upgradeVariantViewPrefab, upgradeVariantViewRoot);
        }
    }
}
