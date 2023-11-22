using Common;
using DataStructs;
using Tank.Towers;
using Tank.UpgradablePiece;
using Tank.Weapons.Projectiles;
using UnityEngine;

namespace Tank.Weapons
{
    [InterfaceEditor]
    public interface IWeapon : IUpgradablePiece
    {
        public void ProceedAttack(float deltaTime);
        public void Initialize(Transform tankRoot, EnemyFinder enemyFinder);
    }

    public interface IHaveDamage
    {
        public ModifiableValue<float> Damage { get; }
    }

    public interface IHaveFireRange
    {
        public ModifiableValue<float> FireRange { get; }
    }

    public interface IHaveProjectileSize
    {
        public ModifiableValue<float> ProjectileSize { get; }
    }

    public interface IHavePenetration
    {
        public ModifiableValue<int> Penetration { get; }
    }

    public interface IHaveProjectilesPerShoot
    {
        public ModifiableValue<int> ProjectilesPerShoot { get; }
    }

    public interface IHaveFireRate
    {
        public ModifiableValue<float> FireRate { get; }
    }

    public interface IHaveCriticalChance
    {
        public ModifiableValue<Percentage> CriticalChance { get; }
        public ModifiableValue<Percentage> CriticalMultiplier { get; }
    }

    public interface IHaveProjectile
    {
        public Projectile ProjectilePrefab { get; }
    }

    public interface IHaveProjectileSpeed
    {
        public ModifiableValue<float> ProjectileSpeed { get; }
    }

    public interface IHaveTower<T>
        where T : ITower
    {
        public T TowerPrefab { get; }
    }

    public interface IHaveRay
    {
        public RayRenderer RayPrefab { get; }
    }
}
