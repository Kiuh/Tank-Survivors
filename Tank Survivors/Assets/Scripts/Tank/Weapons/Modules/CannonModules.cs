using Common;
using DataStructs;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Tank.Weapons.Modules.Cannon
{
    public class CannonModule : IWeaponModule
    {
        [OdinSerialize]
        [HideLabel]
        [AssetList]
        [FoldoutGroup("Cannon Prefab")]
        public Towers.Cannon.Cannon CannonPrefab { get; private set; }
    }

    public class MultiCannonFireRateModule : IWeaponModule
    {
        [OdinSerialize]
        [HideLabel]
        [FoldoutGroup("Cannons Fire Rate Percent")]
        public ModifiableValue<Percentage> Percent { get; private set; }
    }
}
