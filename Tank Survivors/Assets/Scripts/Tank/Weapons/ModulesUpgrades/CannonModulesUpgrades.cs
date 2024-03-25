using System.Collections;
using System.Linq;
using Common;
using DataStructs;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Tank.Towers;
using Tank.Weapons.Modules;

namespace Tank.Weapons.ModulesUpgrades.Cannon
{
    public class AddCannonForRailGun : IModuleUpgrade
    {
        public Towers.Cannon.Positioner CannonPositioner;

        [ValueDropdown("GetAllPositions")]
        public string CannonPosition;

        public void ApplyUpgrade(IWeapon weapon)
        {
            weapon
                .Modules.GetConcrete<TowerModule<SingleShotTower>, IWeaponModule>()
                .Tower.GetComponent<Towers.Cannon.RailGunController>()
                .AddCannon(CannonPosition);
        }

        private IEnumerable GetAllPositions()
        {
            return CannonPositioner?.Properties.Select(x => x.Name);
        }
    }

    public class MultyCannonFireRateModule : IWeaponModule
    {
        [OdinSerialize]
        [HideLabel]
        [FoldoutGroup("Cannons Fire Rate Percent")]
        public ModifiableValue<Percentage> Percent { get; private set; }
    }
}
