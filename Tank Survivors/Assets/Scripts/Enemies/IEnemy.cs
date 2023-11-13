using Tank;

namespace Enemies
{
    public interface IEnemy
    {
        public void Initialize(TankImpl tank);
        public void TakeDamage(float damageAmount);
    }
}
