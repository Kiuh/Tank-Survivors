using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using Tank;
using UnityEngine;

namespace Enemies
{
    public class Mine : SerializedMonoBehaviour, IEnemy
    {
        [SerializeField]
        private Configs.Mine mineConfig;

        [SerializeField]
        [ReadOnly]
        private float health;

        [SerializeField]
        [ReadOnly]
        private float damage;

        [SerializeField]
        [ReadOnly]
        private float explosiveRadius;

        [SerializeField]
        [ReadOnly]
        private TankImpl tank;

        [SerializeField]
        private CircleCollider2D explosiveArea;

        [SerializeField]
        private SpriteRenderer explosiveAreaVisualization;

        [OdinSerialize]
        public string EnemyName { get; private set; }

        public event Action OnDeath;

        public void Initialize(TankImpl tank)
        {
            this.tank = tank;
            health = mineConfig.Health;
            damage = mineConfig.Damage;
            explosiveRadius = mineConfig.ExplosionRadius;
            explosiveArea.radius = explosiveRadius;
            explosiveAreaVisualization.transform.localScale = 2.0f * explosiveRadius * Vector3.one;
            OnDeath += () => tank.EnemyPickupsGenerator.GeneratePickup(this, transform);
            OnDeath += () => Destroy(gameObject);
        }

        public void TakeDamage(float damageAmount)
        {
            health -= damageAmount;
            if (health <= 0)
            {
                health = 0;
                OnDeath?.Invoke();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out TankImpl _))
            {
                tank.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
