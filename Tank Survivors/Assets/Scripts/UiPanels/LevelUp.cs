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
            skipButton.onClick.AddListener(ButtonSkipPress);
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

        public void ButtonSkipPress()
        {
            if (levelUpStack > 0)
            {
                levelUpStack--;
                FillLevelUp();
            }
            else
            {
                levelUpPanel.SetActive(false);
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
                foreach (ILeveledUpgrade variant in upgrade.GetNextUpgrades())
                {
                    UpgradeVariantView upgradeVariant = upgradeBlock.CreateUpgradeVariantView();
                    upgradeVariant.InformationText = variant.Description;
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
