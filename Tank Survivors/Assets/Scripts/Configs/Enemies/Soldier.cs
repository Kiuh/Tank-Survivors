using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(
        fileName = "SoldierConfig",
        menuName = "Configs/EnemiesConfigs/SoldierConfig",
        order = 1
    )]
    public class Soldier : ScriptableObject
    {
        [SerializeField]
        private MovingEnemy movingEnemyConfig;
        public MovingEnemy MovingEnemyConfig => movingEnemyConfig;

        [SerializeField]
        private float damage;
        public float Damage => damage;

        [SerializeField]
        private float timeForNextHit;
        public float TimeForNextHit => timeForNextHit;
    }
}
