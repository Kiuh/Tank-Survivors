using Common;
using Sirenix.OdinInspector;
using Tank.Weapons.Projectiles;
using UnityEngine;

namespace Tank.Weapons.Modules.SelfExplosion
{
    public class FireTimerModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Explosion Fire Time")]
        private ModifiableValue<float> time;
        public ModifiableValue<float> Time => time;
    }

    public class ProjectileModule : IWeaponModule
    {
        [Required]
        [SerializeField]
        [AssetSelector]
        [FoldoutGroup("Explosion Prefab")]
        private SelfExplosionProjectile projectilePrefab;
        public SelfExplosionProjectile ProjectilePrefab => projectilePrefab;
    }

    public class SelfExplosionCountModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Explosion Count")]
        private ModifiableValue<int> count;
        public ModifiableValue<int> Count => count;
    }

    public class FireRateModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Explosion FireRate")]
        private ModifiableValue<float> fireRate;
        public ModifiableValue<float> FireRate => fireRate;
    }

    public class RadiusModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Explosion Radius")]
        private ModifiableValue<float> radius;
        public ModifiableValue<float> Radius => radius;
    }

    public class DamageModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Explosion Damage")]
        private ModifiableValue<float> damage;
        public ModifiableValue<float> Damage => damage;
    }

    public class HitMarkTimerModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Explosion Hit Mark Timer")]
        private ModifiableValue<float> time;
        public ModifiableValue<float> Time => time;
    }
}
