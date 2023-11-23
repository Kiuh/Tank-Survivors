using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using Tank.Weapons;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(
        fileName = "TankWeaponsConfig",
        menuName = "Configs/TankWeaponsConfig",
        order = 3
    )]
    public class TankWeapons : SerializedScriptableObject
    {
        [OdinSerialize]
        [ListDrawerSettings(DraggableItems = false)]
        [LabelText("All Weapons")]
        public List<IWeapon> Weapons { get; private set; }
    }
}
