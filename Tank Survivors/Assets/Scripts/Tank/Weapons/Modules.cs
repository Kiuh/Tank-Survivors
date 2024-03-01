using Common;
using DataStructs;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Tank.Towers;
using Tank.Weapons.Projectiles;

namespace Tank.Weapons
{
    [HideReferenceObjectPicker]
    public interface IWeaponModule { }

    public class DamageModule : IWeaponModule
    {
        [OdinSerialize]
        [HideLabel]
        [FoldoutGroup("Damage")]
        public ModifiableValue<float> Damage { get; private set; } = new();
    }

    public class FireRangeModule : IWeaponModule
    {
        [OdinSerialize]
        [HideLabel]
        [FoldoutGroup("Fire Range")]
        public ModifiableValue<float> FireRange { get; private set; } = new();
    }

    public class ProjectileSizeModule : IWeaponModule
    {
        [OdinSerialize]
        [HideLabel]
        [FoldoutGroup("Projectile Size")]
        public ModifiableValue<float> ProjectileSize { get; private set; } = new();
    }

    public class PenetrationModule : IWeaponModule
    {
        [OdinSerialize]
        [HideLabel]
        [FoldoutGroup("Penetration")]
        public ModifiableValue<int> Penetration { get; private set; } = new();
    }

    public class ProjectilesPerShootModule : IWeaponModule
    {
        [OdinSerialize]
        [HideLabel]
        [FoldoutGroup("Projectiles Per Shoot")]
        public ModifiableValue<int> ProjectilesPerShoot { get; private set; } = new();
    }

    public class FireRateModule : IWeaponModule
    {
        [OdinSerialize]
        [HideLabel]
        [FoldoutGroup("Fire Rate")]
        public ModifiableValue<float> FireRate { get; private set; } = new();
    }

    public class CriticalChanceModule : IWeaponModule
    {
        [OdinSerialize]
        [FoldoutGroup("Critical Chance")]
        public ModifiableValue<Percentage> CriticalChance { get; private set; } = new();
    }

    public class CriticalMultiplierModule : IWeaponModule
    {
        [OdinSerialize]
        [FoldoutGroup("Critical Damage")]
        public ModifiableValue<Percentage> CriticalMultiplier { get; private set; } = new();
    }

    public class ProjectileModule<T> : IWeaponModule
        where T : IProjectile
    {
        [OdinSerialize]
        [HideLabel]
        [AssetList]
        [FoldoutGroup("Projectile")]
        public T ProjectilePrefab { get; private set; }
    }

    public class ProjectileSpeedModule : IWeaponModule
    {
        [OdinSerialize]
        [HideLabel]
        [FoldoutGroup("Projectile Speed")]
        public ModifiableValue<float> ProjectileSpeed { get; private set; } = new();
    }

    public class ProjectileDamageRadiusModule : IWeaponModule
    {
        [OdinSerialize]
        [HideLabel]
        [FoldoutGroup("Damage Radius")]
        public ModifiableValue<float> DamageRadius { get; private set; } = new();
    }

    public class TowerModule<T> : IWeaponModule
        where T : ITower
    {
        [OdinSerialize]
        [HideLabel]
        [FoldoutGroup("Tower")]
        [AssetList]
        public T TowerPrefab { get; private set; }

        [OdinSerialize]
        [HideLabel]
        [FoldoutGroup("Tower")]
        [AssetList]
        [ReadOnly]
        public T Tower { get; set; }
    }

    public class RayDurationModule : IWeaponModule
    {
        [OdinSerialize]
        [HideLabel]
        [FoldoutGroup("Ray Duration")]
        public ModifiableValue<float> RayDuration { get; private set; } = new();
    }

    public class ProjectileSpreadAngleModule : IWeaponModule
    {
        [OdinSerialize]
        [HideLabel]
        [FoldoutGroup("Spread Angle")]
        public ModifiableValue<float> SpreadAngle { get; private set; } = new();
    }

    public class TowerRotationModule : IWeaponModule
    {
        [OdinSerialize]
        [HideLabel]
        [FoldoutGroup("Tower Rotation")]
        public ModifiableValue<float> RotationSpeed { get; private set; } = new();
    }

    public class CannonModule : IWeaponModule
    {
        [OdinSerialize]
        [HideLabel]
        [AssetList]
        [FoldoutGroup("Cannon")]
        public Cannon CannonPrefab { get; private set; }
    }
}
