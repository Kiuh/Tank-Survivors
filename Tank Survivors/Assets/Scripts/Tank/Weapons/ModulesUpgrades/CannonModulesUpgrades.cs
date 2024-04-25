using System;
using Common;
using Sirenix.OdinInspector;
using Tank.Weapons.Modules;
using UnityEngine;

namespace Tank.Weapons.ModulesUpgrades.Cannon
{
    [Serializable]
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
    }
}
