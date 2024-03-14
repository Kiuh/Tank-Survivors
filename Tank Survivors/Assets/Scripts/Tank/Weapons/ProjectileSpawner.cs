using Tank.Towers;
using Tank.Weapons.Projectiles;
using UnityEngine;

namespace Tank.Weapons
{
    public enum SpawnVariation
    {
        Connected,
        Disconnected
    }

    public class ProjectileSpawner
    {
        private GunBase weapon;
        private ITower tower;

        public ProjectileSpawner(GunBase weapon, ITower tower)
        {
            this.weapon = weapon;
            this.tower = tower;
        }

        public IProjectile Spawn(SpawnVariation spawnVariation, Transform parent)
        {
            switch (spawnVariation)
            {
                case SpawnVariation.Connected:
                    return SpawnConnected(parent);
                case SpawnVariation.Disconnected:
                    return Spawn();
                default:
                    return null;
            }
        }

        public IProjectile Spawn()
        {
            return Object.Instantiate(
                    weapon.GetModule<ProjectileModule>().ProjectilePrefab,
                    tower.GetShotPoint(),
                    Quaternion.identity
                ) as IProjectile;
        }

        public IProjectile SpawnConnected(Transform parent)
        {
            return SpawnConnected(weapon.GetModule<ProjectileModule>().ProjectilePrefab, parent);
        }

        public IProjectile SpawnConnected(Transform prefab, Transform parent)
        {
            return Object.Instantiate(prefab, tower.GetShotPoint(), Quaternion.identity, parent)
                as IProjectile;
        }
    }
}
