using UnityEngine;

namespace Tank.Weapons.Projectiles
{
    public interface IProjectile
    {
        public void Initialize(GunBase weapon, TankImpl tank, Vector3 shotPoint, Vector3 direction);
        public void Shoot();
        public IProjectile Spawn();
        public IProjectile SpawnConnected(Transform Parent);
    }
}
