using System.Collections.Generic;
using Sirenix.OdinInspector;
using Tank.Weapons;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(
        fileName = "TankWeaponsConfig",
        menuName = "Configs/TankWeaponsConfig",
        order = 3
    )]
    public class TankWeapons : ScriptableObject
    {
        [SerializeReference]
        [ListDrawerSettings(DraggableItems = false)]
        [LabelText("All Weapons")]
        private List<IWeapon> weapons;
        public List<IWeapon> Weapons => weapons;
    }
}
