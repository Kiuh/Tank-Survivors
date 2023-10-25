using System.Collections.Generic;
using System.Linq;
using Tank.Upgrades;
using UnityEngine;

namespace General.Configs
{
    [CreateAssetMenu(
        fileName = "TankUpgradesConfig",
        menuName = "Configs/TankUpgradesConfig",
        order = 2
    )]
    public class TankUpgradesConfig : ScriptableObject
    {
        [SerializeField]
        private List<SerializedTankUpgrade> tankUpgrades;
        public IEnumerable<ITankUpgrade> TankUpgrades =>
            tankUpgrades.Select(x => x.ToTankUpgrade());
    }
}
