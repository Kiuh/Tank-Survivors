using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using System.Linq;
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
        private List<SerializedTankUpgrade> upgrades;
        public IEnumerable<ITankUpgrade> Upgrades => upgrades.Select(x => x.ToTankUpgrade());
    }
}
