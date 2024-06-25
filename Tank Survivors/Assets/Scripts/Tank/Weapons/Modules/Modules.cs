using Common;
using DataStructs;
using Sirenix.OdinInspector;
using Tank.Towers;
using Tank.Weapons.Projectiles;
using TNRD;
using UnityEngine;

namespace Tank.Weapons.Modules
{
    public interface IWeaponModule { }

    public class DamageModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Damage")]
        private ModifiableValue<float> damage = new();
        public ModifiableValue<float> Damage => damage;
    }

    public class FireRangeModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Fire Range")]
        private ModifiableValue<float> fireRange = new();
        public ModifiableValue<float> FireRange => fireRange;
    }

    public class ProjectileSizeModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Projectile Size")]
        private ModifiableValue<float> projectileSize = new();
        public ModifiableValue<float> ProjectileSize => projectileSize;
    }

    public class PenetrationModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Penetration")]
        private ModifiableValue<int> penetration = new();
        public ModifiableValue<int> Penetration => penetration;
    }

    public class ProjectilesPerShootModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Projectiles Per Shoot")]
        private ModifiableValue<int> projectilesPerShoot = new();
        public ModifiableValue<int> ProjectilesPerShoot => projectilesPerShoot;
    }

    public class FireRateModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Fire Rate")]
        private ModifiableValue<float> fireRate = new();
        public ModifiableValue<float> FireRate => fireRate;
    }

    public class CriticalChanceModule : IWeaponModule
    {
        [SerializeField]
        [FoldoutGroup("Critical Chance")]
        private ModifiableValue<Percentage> criticalChance = new();
        public ModifiableValue<Percentage> CriticalChance => criticalChance;
    }

    public class CriticalMultiplierModule : IWeaponModule
    {
        [SerializeField]
        [FoldoutGroup("Critical Damage")]
        private ModifiableValue<Percentage> criticalMultiplier = new();
        public ModifiableValue<Percentage> CriticalMultiplier => criticalMultiplier;
    }

    public class ProjectileModule : IWeaponModule
    {
        [SerializeField]
        [FoldoutGroup("Projectile")]
        private SerializableInterface<IProjectile> projectilePrefab;
        public IProjectile ProjectilePrefab => projectilePrefab.Value;
    }

    public class ProjectileSpeedModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Projectile Speed")]
        private ModifiableValue<float> projectileSpeed = new();
        public ModifiableValue<float> ProjectileSpeed => projectileSpeed;
    }

    public class ProjectileDamageRadiusModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Damage Radius")]
        private ModifiableValue<float> damageRadius = new();
        public ModifiableValue<float> DamageRadius => damageRadius;
    }

    public class TowerModule : IWeaponModule
    {
        [SerializeField]
        [FoldoutGroup("Tower")]
        private SerializableInterface<ITower> towerPrefab;
        public ITower TowerPrefab => towerPrefab.Value;

        [SerializeField]
        [FoldoutGroup("Tower")]
        [ReadOnly]
        private SerializableInterface<ITower> tower;
        public ITower Tower => tower.Value;
    }

    public class RayDurationModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Ray Duration")]
        private ModifiableValue<float> rayDuration = new();
        public ModifiableValue<float> RayDuration => rayDuration;
    }

    public class RayFireRateModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Ray Fire Rate")]
        private ModifiableValue<float> fireRate = new();
        public ModifiableValue<float> FireRate => fireRate;
    }

    public class ProjectileSpreadAngleModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Spread Angle")]
        private ModifiableValue<float> spreadAngle = new();
        public ModifiableValue<float> SpreadAngle => spreadAngle;
    }

    public class TowerRotationModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Tower Rotation")]
        private ModifiableValue<float> rotationSpeed = new();
        public ModifiableValue<float> RotationSpeed => rotationSpeed;
    }

    public class FireDamageModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Fire Damage")]
        private ModifiableValue<float> damage;
        public ModifiableValue<float> Damage => damage;
    }

    public class FireFireRateModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Fire Fire Rate")]
        private ModifiableValue<float> fireRate;
        public ModifiableValue<float> FireRate => fireRate;
    }

    public class ProjectileFireTimerModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Projectile Fire Time")]
        private ModifiableValue<float> time;
        public ModifiableValue<float> Time => time;
    }
}
