using System.Collections.Generic;
using System.Linq;
using Common;
using Sirenix.OdinInspector;
using Tank;
using Tank.UpgradablePiece;
using Tank.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace Panels.LevelUp
{
    [AddComponentMenu("Panels.LevelUp.LevelUpMain")]
    public class LevelUpMain : MonoBehaviour
    {
        [SerializeField]
        private TankImpl tank;

        [SerializeField]
        private UpgradeBlock upgradeBlockPrefab;

        [SerializeField]
        private RectTransform upgradeBlocksViewRoot;

        [SerializeField]
        private GameObject levelUpPanel;

        [SerializeField]
        private Button skipButton;

        [SerializeField]
        [ReadOnly]
        private uint levelUpStack = 0;

        private void OnEnable()
        {
            tank.PlayerLevel.OnLevelUp += LevelUpRelease;
            skipButton.onClick.AddListener(ButtonSkipPress);
        }

        public void LevelUpRelease()
        {
            Time.timeScale = 0.0f;
            if (!levelUpPanel.activeSelf)
            {
                levelUpPanel.SetActive(true);
                UpdateUpgradesList();
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
                UpdateUpgradesList();
            }
            else
            {
                Time.timeScale = 1.0f;
                levelUpPanel.SetActive(false);
            }
        }

        private void UpdateUpgradesList()
        {
            foreach (RectTransform child in upgradeBlocksViewRoot)
            {
                Destroy(child.gameObject);
            }
            skipButton.gameObject.SetActive(false);

            List<IUpgradablePiece> upgrades = tank.GetLevelUpUpgrades().ToList();
            if (upgrades == null || upgrades.Count <= 0)
            {
                skipButton.gameObject.SetActive(true);
                upgrades = tank.GetAvailableUpgrades()
                    .ToList()
                    .TakeRandom(tank.LevelUpChoicesCount.GetModifiedValue());
                foreach (IUpgradablePiece upgrade in upgrades)
                {
                    UpgradeBlock upgradeBlock = CreateUpgradeBlock(upgrade);
                    foreach (ILeveledUpgrade variant in upgrade.GetNextUpgrades())
                    {
                        SetupUpgradeVariant(upgrade, variant, upgradeBlock);
                    }
                }
                return;
            }

            foreach (IWeapon upgrade in upgrades)
            {
                foreach (ILevelUpUpgrade variant in upgrade.LevelUpUpgrades)
                {
                    if (!variant.LevelForUpgrade.Equals(tank.PlayerLevel.CurrentLevel))
                    {
                        continue;
                    }
                    UpgradeBlock upgradeBlock = CreateUpgradeBlock(upgrade);
                    SetupUpgradeVariant(upgrade, variant, upgradeBlock);
                }
            }
        }

        private void SetupUpgradeVariant(
            IUpgradablePiece upgrade,
            ILevelUpgrade variant,
            UpgradeBlock upgradeBlock
        )
        {
            UpgradeVariant upgradeVariant = upgradeBlock.CreateUpgradeVariantView();
            upgradeVariant.InformationText = variant.Description;
            upgradeVariant.Button.onClick.AddListener(() =>
            {
                upgrade.ApplyUpgrade(tank, variant);
                if (levelUpStack > 0)
                {
                    levelUpStack--;
                    UpdateUpgradesList();
                }
                else
                {
                    levelUpPanel.SetActive(false);
                    Time.timeScale = 1.0f;
                }
            });
        }

        private UpgradeBlock CreateUpgradeBlock(IUpgradablePiece upgrade)
        {
            UpgradeBlock upgradeBlock = Instantiate(upgradeBlockPrefab, upgradeBlocksViewRoot);
            upgradeBlock.UpgradeName = upgrade.UpgradeName;
            return upgradeBlock;
        }
    }
}
