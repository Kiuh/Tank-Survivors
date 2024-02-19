using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Tank;
using UnityEngine;

namespace Enemies
{
    public class Mine : SerializedMonoBehaviour, IEnemy
    {
        [SerializeField]
        [ReadOnly]
        [InlineProperty]
        private Configs.MineConfig clonedConfig;

        [SerializeField]
        Configs.Mine config;

        [SerializeField]
        [ReadOnly]
        private TankImpl tank;

        [SerializeField]
        private CircleCollider2D explosiveArea;

        [SerializeField]
        private SpriteRenderer explosiveAreaVisualization;

        [SerializeField]
        private ParticleSystem particle;

        [SerializeField]
        private SpriteRenderer sprite;

        [OdinSerialize]
        public string EnemyName { get; private set; }

        public event Action OnDeath;

        public void Initialize(TankImpl tank)
        {
            this.tank = tank;

            clonedConfig = config.Config;

            explosiveArea.radius = clonedConfig.ExplosionRadius;
            explosiveAreaVisualization.transform.localScale =
                2.0f * clonedConfig.ExplosionRadius * Vector3.one;

            OnDeath += () => tank.EnemyPickupsGenerator.GeneratePickup(this, transform);
            OnDeath += () => Destroy(gameObject);
        }

        public void TakeDamage(float damageAmount)
        {
            clonedConfig.Health -= damageAmount;
            if (clonedConfig.Health <= 0)
            {
                clonedConfig.Health = 0;
                OnDeath?.Invoke();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out TankImpl _))
            {
                particle.transform.localScale = new Vector3(
                    clonedConfig.ExplosionRadius,
                    clonedConfig.ExplosionRadius,
                    1.0f
                );
                particle.Play();
                explosiveArea.enabled = false;
                sprite.enabled = false;
                tank.TakeDamage(clonedConfig.Damage);
                Destroy(gameObject, particle.main.duration * particle.main.startLifetimeMultiplier);
            }
        }
    }
}
