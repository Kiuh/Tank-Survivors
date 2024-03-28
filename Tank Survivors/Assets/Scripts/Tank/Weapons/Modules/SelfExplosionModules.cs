using Common;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Tank.Weapons.Projectiles;

namespace Tank.Weapons.Modules.SelfExplosion
{
    public class FireTimerModule : IWeaponModule
    {
        [OdinSerialize]
        [HideLabel]
        [FoldoutGroup("Explosion Fire Time")]
        public ModifiableValue<float> Time { get; private set; }
    }

    public class ProjectileModule : IWeaponModule
    {
        [OdinSerialize]
        [HideLabel]
        [FoldoutGroup("Explosion Prefab")]
        public SelfExplosionProjectile ProjectilePrefab { get; private set; }
    }

    public class SelfExplosionCountModule : IWeaponModule
    {
        [OdinSerialize]
        [HideLabel]
        [FoldoutGroup("Explosion Count")]
        public ModifiableValue<int> Count { get; private set; }
    }

    public class FireRateModule : IWeaponModule
    {
        [OdinSerialize]
        [HideLabel]
        [FoldoutGroup("Explosion FireRate")]
        public ModifiableValue<float> FireRate { get; private set; }
    }

    public class RadiusModule : IWeaponModule
    {
        [OdinSerialize]
        [HideLabel]
        [FoldoutGroup("Explosion Radius")]
        public ModifiableValue<float> Radius { get; private set; }
    }

    public class DamageModule : IWeaponModule
    {
        [OdinSerialize]
        [HideLabel]
        [FoldoutGroup("Explosion Damage")]
        public ModifiableValue<float> Damage { get; private set; }
    }

    public class HitMarkTimerModule : IWeaponModule
    {
        [OdinSerialize]
        [HideLabel]
        [FoldoutGroup("Explosion Hit Mark Timer")]
        public ModifiableValue<float> Time { get; private set; }
    }
}
