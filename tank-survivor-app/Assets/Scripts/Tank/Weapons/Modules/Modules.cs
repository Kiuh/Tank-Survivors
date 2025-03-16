using Common;
using DataStructs;
using Sirenix.OdinInspector;
using Tank.Towers;
using Tank.Weapons.Projectiles;
using TNRD;
using UnityEngine;

namespace Tank.Weapons.Modules
{
    public interface IWeaponModule
    {
        public object[] Values { get; }
    }

    public class DamageModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Damage")]
        private ModifiableValue<float> damage = new();
        public ModifiableValue<float> Damage => damage;

        public object[] Values => new object[] { Damage.Value };
    }

    public class FireRangeModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Fire Range")]
        private ModifiableValue<float> fireRange = new();
        public ModifiableValue<float> FireRange => fireRange;

        public object[] Values => new object[] { FireRange.Value };
    }

    public class ProjectileSizeModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Projectile Size")]
        private ModifiableValue<float> projectileSize = new();
        public ModifiableValue<float> ProjectileSize => projectileSize;

        public object[] Values => new object[] { ProjectileSize.Value };
    }

    public class PenetrationModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Penetration")]
        private ModifiableValue<int> penetration = new();
        public ModifiableValue<int> Penetration => penetration;

        public object[] Values => new object[] { Penetration.Value };
    }

    public class ProjectilesPerShootModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Projectiles Per Shoot")]
        private ModifiableValue<int> projectilesPerShoot = new();
        public ModifiableValue<int> ProjectilesPerShoot => projectilesPerShoot;

        public object[] Values => new object[] { ProjectilesPerShoot.Value };
    }

    public class FireRateModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Fire Rate")]
        private ModifiableValue<float> fireRate = new();
        public ModifiableValue<float> FireRate => fireRate;

        public object[] Values => new object[] { FireRate.Value };
    }

    public class CriticalChanceModule : IWeaponModule
    {
        [SerializeField]
        [FoldoutGroup("Critical Chance")]
        private ModifiableValue<Percentage> criticalChance = new();
        public ModifiableValue<Percentage> CriticalChance => criticalChance;

        public object[] Values => new object[] { CriticalChance.Value.Value };
    }

    public class CriticalMultiplierModule : IWeaponModule
    {
        [SerializeField]
        [FoldoutGroup("Critical Damage")]
        private ModifiableValue<Percentage> criticalMultiplier = new();
        public ModifiableValue<Percentage> CriticalMultiplier => criticalMultiplier;

        public object[] Values => new object[] { CriticalMultiplier.Value.Value };
    }

    public class ProjectileModule : IWeaponModule
    {
        [SerializeField]
        [FoldoutGroup("Projectile")]
        private SerializableInterface<IProjectile> projectilePrefab;
        public IProjectile ProjectilePrefab => projectilePrefab.Value;

        public object[] Values =>
            new object[] { (projectilePrefab.Value as MonoBehaviour).gameObject.name };
    }

    public class ProjectileSpeedModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Projectile Speed")]
        private ModifiableValue<float> projectileSpeed = new();
        public ModifiableValue<float> ProjectileSpeed => projectileSpeed;

        public object[] Values => new object[] { ProjectileSpeed.Value };
    }

    public class ProjectileDamageRadiusModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Damage Radius")]
        private ModifiableValue<float> damageRadius = new();
        public ModifiableValue<float> DamageRadius => damageRadius;

        public object[] Values => new object[] { DamageRadius.Value };
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

        public object[] Values =>
            new object[] { (towerPrefab.Value as MonoBehaviour).gameObject.name };

        public void SetNewTower(ITower tower)
        {
            this.tower = new SerializableInterface<ITower>(tower);
        }

        public void RemoveTower()
        {
            tower = null;
        }
    }

    public class RayDurationModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Ray Duration")]
        private ModifiableValue<float> rayDuration = new();
        public ModifiableValue<float> RayDuration => rayDuration;

        public object[] Values => new object[] { RayDuration.Value };
    }

    public class RayFireRateModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Ray Fire Rate")]
        private ModifiableValue<float> fireRate = new();
        public ModifiableValue<float> FireRate => fireRate;

        public object[] Values => new object[] { FireRate.Value };
    }

    public class ProjectileSpreadAngleModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Spread Angle")]
        private ModifiableValue<float> spreadAngle = new();
        public ModifiableValue<float> SpreadAngle => spreadAngle;

        public object[] Values => new object[] { SpreadAngle.Value };
    }

    public class TowerRotationModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Tower Rotation")]
        private ModifiableValue<float> rotationSpeed = new();
        public ModifiableValue<float> RotationSpeed => rotationSpeed;

        public object[] Values => new object[] { RotationSpeed.Value };
    }

    public class FireDamageModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Fire Damage")]
        private ModifiableValue<float> damage;
        public ModifiableValue<float> Damage => damage;

        public object[] Values => new object[] { Damage.Value };
    }

    public class FireFireRateModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Fire Fire Rate")]
        private ModifiableValue<float> fireRate;
        public ModifiableValue<float> FireRate => fireRate;

        public object[] Values => new object[] { FireRate.Value };
    }

    public class ProjectileFireTimerModule : IWeaponModule
    {
        [SerializeField]
        [HideLabel]
        [FoldoutGroup("Projectile Fire Time")]
        private ModifiableValue<float> time;
        public ModifiableValue<float> Time => time;

        public object[] Values => new object[] { Time.Value };
    }
}
