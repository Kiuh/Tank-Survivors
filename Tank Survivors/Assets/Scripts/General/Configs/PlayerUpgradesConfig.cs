using System.Collections.Generic;
using System.Linq;
using Tank.Upgrades;
using UnityEngine;

namespace General.Configs
{
    [CreateAssetMenu(
        fileName = "PlayerUpgradesConfig",
        menuName = "Configs/PlayerUpgradesConfig",
        order = 2
    )]
    public class PlayerUpgradesConfig : ScriptableObject
    {
        [SerializeField]
        private List<SerializedTankUpgrade> tankUpgrades;
        public IEnumerable<ITankUpgrade> TankUpgrades =>
            tankUpgrades.Select(x => x.ToTankUpgrade());
    }
}
