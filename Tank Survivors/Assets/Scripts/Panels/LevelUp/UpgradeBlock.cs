using TMPro;
using UnityEngine;

namespace Panels.LevelUp
{
    [AddComponentMenu("Panels.LevelUp.UpgradeBlock")]
    public class UpgradeBlock : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text upgradeNameLabel;
        public string UpgradeName
        {
            get => upgradeNameLabel.text;
            set => upgradeNameLabel.text = value;
        }

        [SerializeField]
        private UpgradeVariant upgradeVariantPrefab;

        [SerializeField]
        private Transform upgradeVariantViewRoot;

        public UpgradeVariant CreateUpgradeVariantView()
        {
            return Instantiate(upgradeVariantPrefab, upgradeVariantViewRoot);
        }
    }
}
