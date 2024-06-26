using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Panels.LevelUp
{
    [AddComponentMenu("Panels.LevelUp.UpgradeBlock")]
    public class UpgradeBlock : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private TMP_Text upgradeNameLabel;
        public string UpgradeName
        {
            get => upgradeNameLabel.text;
            set => upgradeNameLabel.text = value;
        }

        [Required]
        [SerializeField]
        private UpgradeVariant upgradeVariantPrefab;

        [Required]
        [SerializeField]
        private Transform upgradeVariantViewRoot;

        public UpgradeVariant CreateUpgradeVariantView()
        {
            return Instantiate(upgradeVariantPrefab, upgradeVariantViewRoot);
        }
    }
}
