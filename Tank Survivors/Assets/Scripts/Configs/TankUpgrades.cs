using System.Collections.Generic;
using Sirenix.OdinInspector;
using Tank.Upgrades;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(
        fileName = "TankUpgradesConfig",
        menuName = "Configs/TankUpgradesConfig",
        order = 2
    )]
    public class TankUpgrades : ScriptableObject
    {
        [SerializeField]
        [ListDrawerSettings(DraggableItems = false)]
        [LabelText("All Tank Upgrades")]
        private List<TankUpgrade> upgrades;
        public List<TankUpgrade> Upgrades
        {
            get => upgrades;
            private set => upgrades = value;
        }
    }
}
