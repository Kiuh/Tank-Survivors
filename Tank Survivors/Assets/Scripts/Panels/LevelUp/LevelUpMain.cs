using System.Collections.Generic;
using System.Linq;
using Audio;
using Common;
using DG.Tweening;
using Sirenix.OdinInspector;
using Tank;
using Tank.UpgradablePiece;
using Tank.Weapons;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Panels.LevelUp
{
    [AddComponentMenu("Panels.LevelUp.LevelUpMain")]
    public class LevelUpMain : MonoBehaviour
    {
        [RequiredIn(PrefabKind.PrefabInstanceAndNonPrefabInstance)]
        [SerializeField]
        private TankImpl tank;

        [Required]
        [SerializeField]
        private UpgradeBlock upgradeBlockPrefab;

        [Required]
        [SerializeField]
        private RectTransform upgradeBlocksViewRoot;

        [Required]
        [SerializeField]
        private GameObject levelUpPanel;

        [SerializeField]
        [ReadOnly]
        private uint levelUpStack = 0;

        [SerializeField]
        private float resumeDelay;

        [SerializeField]
        private float startAlfa;

        [Required]
        [SerializeField]
        private Image lerpImage;

        public UnityEvent OnLevelUpPanelOpen;

        private void OnEnable()
        {
            tank.PlayerLevel.OnLevelUp += LevelUpRelease;
        }

        public void LevelUpRelease()
        {
            SoundsManager.Instance.PauseSounds();
            Time.timeScale = 0.0f;
            tank.StopScreenEffects();
            OnLevelUpPanelOpen?.Invoke();
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

        private void UpdateUpgradesList()
        {
            foreach (RectTransform child in upgradeBlocksViewRoot)
            {
                Destroy(child.gameObject);
            }

            IEnumerable<IUpgradablePiece> upgrades = tank.GetLevelUpUpgrades();
            if (upgrades == null || upgrades.Count() <= 0)
            {
                upgrades = tank.GetAvailableUpgrades()
                    .ToList()
                    .TakeRandom(tank.LevelUpChoicesCount.GetModifiedValue());

                if (upgrades == null || upgrades.Count() <= 0)
                {
                    HandleLevelUp();
                    return;
                }

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
                HandleLevelUp();
            });
        }

        private Tween delayTween;

        private void HandleLevelUp()
        {
            if (levelUpStack > 0)
            {
                levelUpStack--;
                UpdateUpgradesList();
            }
            else
            {
                levelUpPanel.SetActive(false);
                lerpImage.gameObject.SetActive(true);
                lerpImage.color = new Color(
                    lerpImage.color.r,
                    lerpImage.color.g,
                    lerpImage.color.b,
                    startAlfa
                );
                delayTween = lerpImage
                    .DOFade(0, resumeDelay)
                    .SetUpdate(true)
                    .OnComplete(() =>
                    {
                        Time.timeScale = 1.0f;
                        SoundsManager.Instance.UnPauseSounds();
                        lerpImage.gameObject.SetActive(false);
                    });
            }
        }

        private UpgradeBlock CreateUpgradeBlock(IUpgradablePiece upgrade)
        {
            UpgradeBlock upgradeBlock = Instantiate(upgradeBlockPrefab, upgradeBlocksViewRoot);
            upgradeBlock.UpgradeName = upgrade.UpgradeName;
            return upgradeBlock;
        }

        private void OnDestroy()
        {
            delayTween?.Kill();
        }
    }
}
