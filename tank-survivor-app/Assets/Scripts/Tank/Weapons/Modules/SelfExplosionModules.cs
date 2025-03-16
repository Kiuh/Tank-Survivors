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

        public object[] Values => new object[] { Time.Value };
    }

    public class ProjectileModule : IWeaponModule
    {
        [Required]
        [SerializeField]
        [AssetSelector]
        [FoldoutGroup("Explosion Prefab")]
        private SelfExplosionProjectile projectilePrefab;
        public SelfExplosionProjectile ProjectilePrefab => projectilePrefab;

        public object[] Values => new object[] { ProjectilePrefab.gameObject.name };
    }

    public class SelfExplosionCountModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Explosion Count")]
        private ModifiableValue<int> count;
        public ModifiableValue<int> Count => count;

        public object[] Values => new object[] { Count.Value };
    }

    public class FireRateModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Explosion FireRate")]
        private ModifiableValue<float> fireRate;
        public ModifiableValue<float> FireRate => fireRate;

        public object[] Values => new object[] { FireRate.Value };
    }

    public class RadiusModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Explosion Radius")]
        private ModifiableValue<float> radius;
        public ModifiableValue<float> Radius => radius;

        public object[] Values => new object[] { Radius.Value };
    }

    public class DamageModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Explosion Damage")]
        private ModifiableValue<float> damage;
        public ModifiableValue<float> Damage => damage;

        public object[] Values => new object[] { Damage.Value };
    }

    public class HitMarkTimerModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Explosion Hit Mark Timer")]
        private ModifiableValue<float> time;
        public ModifiableValue<float> Time => time;

        public object[] Values => new object[] { Time.Value };
    }
}
