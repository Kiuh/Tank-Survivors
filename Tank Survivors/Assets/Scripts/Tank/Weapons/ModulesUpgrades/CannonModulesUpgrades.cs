using System.Collections;
using System.Linq;
using Common;
using Sirenix.OdinInspector;
using Tank.Weapons.Modules;
using UnityEngine;

namespace Tank.Weapons.ModulesUpgrades.Cannon
{
    public class AddCannonForRailGun : IModuleUpgrade
    {
        public Towers.Cannon.Positioner CannonPositioner;

        [ValueDropdown("GetAllPositions")]
        public string CannonPosition;

        public void ApplyUpgrade(IWeapon weapon)
        {
            MonoBehaviour tower =
                weapon.Modules.GetConcrete<TowerModule, IWeaponModule>().Tower as MonoBehaviour;
            tower.GetComponent<Towers.Cannon.Controller>().AddCannon(CannonPosition);
        }

        private IEnumerable GetAllPositions()
        {
            return CannonPositioner?.Properties.Select(x => x.Name);
        }
    }
}
