using System.Collections;
using System.Linq;
using Common;
using Sirenix.OdinInspector;
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

    public class AddCannonForDoubleGun : IModuleUpgrade
    {
        public Towers.Cannon.Positioner CannonPositioner;

        [ValueDropdown("GetAllPositions")]
        public string CannonPosition;

        public void ApplyUpgrade(IWeapon weapon)
        {
            weapon
                .Modules.GetConcrete<TowerModule<DoubleShotTower>, IWeaponModule>()
                .Tower.GetComponent<Towers.Cannon.DoubleGunController>()
                .AddCannon(CannonPosition);
        }

        private IEnumerable GetAllPositions()
        {
            return CannonPositioner?.Properties.Select(x => x.Name);
        }
    }
}
