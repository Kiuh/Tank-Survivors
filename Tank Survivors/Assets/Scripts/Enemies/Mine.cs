using System;
using System.Collections.Generic;
using Common;
using Configs;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Tank;
using UnityEngine;

namespace Enemies
{
    public class Mine : SerializedMonoBehaviour, IEnemy
    {
        [OdinSerialize]
        private SpriteRenderer dangerZone;

        [SerializeField]
        private ParticleSystem particle;

        [OdinSerialize]
        public string EnemyName { get; private set; }

        [OdinSerialize]
        [ListDrawerSettings(
            HideAddButton = true,
            HideRemoveButton = true,
            AlwaysAddDefaultValue = true,
            DraggableItems = false
        )]
        [FoldoutGroup("Modules")]
        public List<IModule> Modules { get; set; } = new();

        public event Action OnDeath;
        private MineConfig stats;
        private TankImpl tank;
        private bool isExploded;

        public void Initialize(TankImpl tank)
        {
            stats.Health = Modules.GetConcrete<HealthModule, IModule>().Health.GetModifiedValue();
            stats.Damage = Modules.GetConcrete<DamageModule, IModule>().Damage.GetModifiedValue();
            stats.ExplosionRadius = Modules
                .GetConcrete<ExplosionModule, IModule>()
                .Radius.GetModifiedValue();
            dangerZone.transform.localScale = 2 * stats.ExplosionRadius * Vector3.one;
            dangerZone.enabled = true;
            this.tank = tank;
            isExploded = false;
            OnDeath += () => Destroy(gameObject);
        }

        public void TakeDamage(float damageAmount)
        {
            stats.Health -= damageAmount;
            if (stats.Health <= 0)
            {
                stats.Health = 0;
                OnDeath?.Invoke();
            }
        }

        private void Update()
        {
            if (!isExploded)
            {
                CheckZone();
            }
        }

        private void CheckZone()
        {
            RaycastHit2D hit = Physics2D.CircleCast(
                transform.position,
                stats.ExplosionRadius,
                Vector3.zero
            );
            if (hit && hit.transform.gameObject.GetComponent<TankImpl>())
            {
                DealDamage();
            }
        }

        private void DealDamage()
        {
            particle.transform.localScale = new Vector3(
                stats.ExplosionRadius,
                stats.ExplosionRadius,
                1.0f
            );
            isExploded = true;
            particle.Play();
            tank.TakeDamage(stats.Damage);
            Destroy(gameObject, particle.main.duration * particle.main.startLifetimeMultiplier);
        }

        [Button]
        [FoldoutGroup("Modules")]
        private void RefreshModules()
        {
            Modules = new() { new ExplosionModule(), new DamageModule(), new HealthModule() };
        }
    }
}
