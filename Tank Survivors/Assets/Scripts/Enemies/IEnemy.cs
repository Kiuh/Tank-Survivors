using Configs;
using Tank;

namespace Enemies
{
    public interface IEnemy
    {
        public string EnemyName { get; }
        public void Initialize(TankImpl tank, IEnemyConfig config);
        public void TakeDamage(float damageAmount);
    }
}
