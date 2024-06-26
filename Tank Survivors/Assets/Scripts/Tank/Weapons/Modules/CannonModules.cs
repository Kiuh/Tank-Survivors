using Common;
using DataStructs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tank.Weapons.Modules.Cannon
{
    public class CannonModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [AssetList]
        [FoldoutGroup("Cannon Prefab")]
        private Towers.Cannon.Cannon cannonPrefab;
        public Towers.Cannon.Cannon CannonPrefab => cannonPrefab;
    }

    public class MultiCannonFireRateModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Cannons Fire Rate Percent")]
        private ModifiableValue<Percentage> percent;
        public ModifiableValue<Percentage> Percent => percent;
    }
}
