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
}
