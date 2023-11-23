using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using Tank.Upgrades;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(
        fileName = "TankUpgradesConfig",
        menuName = "Configs/TankUpgradesConfig",
        order = 2
    )]
    public class TankUpgrades : SerializedScriptableObject
    {
        [OdinSerialize]
        [ListDrawerSettings(DraggableItems = false)]
        [LabelText("All Tank Upgrades")]
        public List<TankUpgrade> Upgrades { get; private set; }
    }
}
