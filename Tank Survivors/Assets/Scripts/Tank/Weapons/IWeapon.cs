using Common;
using DataStructs;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using Tank.Towers;
using Tank.UpgradablePiece;
using Tank.Weapons.Projectiles;
using UnityEngine;

namespace Tank.Weapons
{
    [HideReferenceObjectPicker]
    public interface IWeapon : IUpgradablePiece
    {
        public void ProceedAttack(float deltaTime);
        public void Initialize(Transform tankRoot, EnemyFinder enemyFinder);
        public abstract List<IWeaponModule> Modules { get; }
    }

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
        [FoldoutGroup("Critical Shoot")]
        public ModifiableValue<Percentage> CriticalChance { get; private set; } = new();

        [OdinSerialize]
        [FoldoutGroup("Critical Shoot")]
        public ModifiableValue<Percentage> CriticalMultiplier { get; private set; } = new();
    }

    public class ProjectileModule<T> : IWeaponModule
        where T : IProjectile
    {
        [OdinSerialize]
        [HideLabel]
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
        public T TowerPrefab { get; private set; }
    }
}
