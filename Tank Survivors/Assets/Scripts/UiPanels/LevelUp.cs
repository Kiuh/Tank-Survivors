using Common;
using System.Collections.Generic;
using System.Linq;
using Tank;
using Tank.UpgradablePiece;
using UnityEngine;
using UnityEngine.UI;

namespace UiPanels
{
    [AddComponentMenu("UiPanels.LevelUp")]
    public class LevelUp : MonoBehaviour
    {
        [SerializeField]
        private TankImpl tank;

        [SerializeField]
        private UpgradeBlockView upgradeBlockViewPrefab;

        [SerializeField]
        private Transform upgradeBlocksViewRoot;

        [SerializeField]
        private GameObject levelUpPanel;

        [SerializeField]
        private Button skipButton;

        [SerializeField]
        [InspectorReadOnly]
        private uint levelUpStack = 0;

        private void Awake()
        {
            tank.PlayerLevel.OnLevelUp += LevelUpRelease;
            skipButton.onClick.AddListener(() => levelUpPanel.SetActive(false));
        }

        public void LevelUpRelease()
        {
            if (!levelUpPanel.activeSelf)
            {
                levelUpPanel.SetActive(true);
                FillLevelUp();
            }
            else
            {
                levelUpStack++;
            }
        }

        private void FillLevelUp()
        {
            List<IUpgradablePiece> upgrades = tank.GetAvailableUpgrades()
                .ToList()
                .TakeRandom(tank.LevelUpChoicesCount.GetModifiedValue());
            while (upgradeBlocksViewRoot.childCount > 0)
            {
                Destroy(upgradeBlocksViewRoot.GetChild(0).gameObject);
            }
            foreach (IUpgradablePiece upgrade in upgrades)
            {
                UpgradeBlockView upgradeBlock = Instantiate(
                    upgradeBlockViewPrefab,
                    upgradeBlocksViewRoot
                );
                upgradeBlock.UpgradeName = upgrade.UpgradeName;
                foreach (UpgradeVariantInformation variant in upgrade.GetNextUpgradeInformation())
                {
                    UpgradeVariantView upgradeVariant = upgradeBlock.CreateUpgradeVariantView();
                    upgradeVariant.InformationText = variant.UpgradeInformation;
                    upgradeVariant.Button.onClick.AddListener(() =>
                    {
                        upgrade.ApplyUpgrade(tank, variant);
                        if (levelUpStack > 0)
                        {
                            levelUpStack--;
                            FillLevelUp();
                        }
                        else
                        {
                            levelUpPanel.SetActive(false);
                        }
                    });
                }
            }
        }
    }
}
