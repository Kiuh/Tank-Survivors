using Common;
using System.Collections.Generic;
using System.Linq;
using Tank;
using Tank.UpgradablePiece;
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
        private Transform upgradeBlocksViewRoot;

        [SerializeField]
        private GameObject levelUpPanel;

        [SerializeField]
        private Button skipButton;

        [SerializeField]
        [InspectorReadOnly]
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
            List<IUpgradablePiece> upgrades = tank.GetAvailableUpgrades()
                .ToList()
                .TakeRandom(tank.LevelUpChoicesCount.GetModifiedValue());
            while (upgradeBlocksViewRoot.childCount > 0)
            {
                Destroy(upgradeBlocksViewRoot.GetChild(0).gameObject);
            }
            foreach (IUpgradablePiece upgrade in upgrades)
            {
                UpgradeBlock upgradeBlock = Instantiate(upgradeBlockPrefab, upgradeBlocksViewRoot);
                upgradeBlock.UpgradeName = upgrade.UpgradeName;
                foreach (ILeveledUpgrade variant in upgrade.GetNextUpgrades())
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
            }
        }
    }
}
