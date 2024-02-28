using Tank.Towers;
using Tank.Weapons.Projectiles;
using UnityEngine;

namespace Tank.Weapons
{
    public class ProjectileSpawner
    {
        private GunBase weapon;
        private ITower tower;

        public ProjectileSpawner(GunBase weapon, ITower tower)
        {
            this.weapon = weapon;
            this.tower = tower;
        }

        public T Spawn<T>()
            where T : MonoBehaviour, IProjectile
        {
            return UnityEngine.Object.Instantiate(
                weapon.GetModule<ProjectileModule<T>>().ProjectilePrefab,
                tower.GetShotPoint(),
                Quaternion.identity
            );
        }
    }
}
