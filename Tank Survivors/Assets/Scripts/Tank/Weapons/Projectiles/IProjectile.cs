using Tank.Towers;

namespace Tank.Weapons.Projectiles
{
    public interface IProjectile
    {
        public void Initialize(GunBase weapon, TankImpl tank, ITower tower);
        public void Shoot();
    }
}
