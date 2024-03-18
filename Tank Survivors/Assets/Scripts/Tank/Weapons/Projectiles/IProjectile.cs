using Tank.Towers;
using UnityEngine;

namespace Tank.Weapons.Projectiles
{
    public interface IProjectile
    {
        public void Initialize(GunBase weapon, TankImpl tank, ITower tower);
        public void Shoot();

        public IProjectile Spawn();
        public IProjectile SpawnConnected(Transform Parent);
    }
}
