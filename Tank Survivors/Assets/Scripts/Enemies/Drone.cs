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
    [AddComponentMenu("Enemies.Drone")]
    public class Drone : SerializedMonoBehaviour, IEnemy
    {
        [OdinSerialize]
        private Rigidbody2D rigidBody;

        [OdinSerialize]
        private SpriteRenderer dangerZone;

        [OdinSerialize]
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

        private TankImpl tank;
        private bool isMoving;
        private DroneConfig stats;

        public void Initialize(TankImpl tank)
        {
            stats.Health = Modules.GetConcrete<HealthModule, IModule>().Health.GetModifiedValue();
            stats.Damage = Modules.GetConcrete<DamageModule, IModule>().Damage.GetModifiedValue();
            stats.ExperienceDropAmount = Modules
                .GetConcrete<ExperienceModule, IModule>()
                .DropAmount.GetModifiedValue();
            stats.ExplosionRadius = Modules
                .GetConcrete<ExplosionModule, IModule>()
                .Radius.GetModifiedValue();
            stats.MovementSpeed = Modules
                .GetConcrete<MovementModule, IModule>()
                .Speed.GetModifiedValue();
            dangerZone.transform.localScale *= stats.ExplosionRadius;
            dangerZone.enabled = true;
            this.tank = tank;
            isMoving = true;
            OnDeath += () => tank.EnemyPickupsGenerator.GeneratePickup(this, transform);
            OnDeath += () => Destroy(gameObject);
        }

        public void FixedUpdate()
        {
            if (isMoving)
            {
                Vector2 direction = GetDirectionToTank();
                RotateToTank(direction);
                rigidBody.MovePosition(
                    rigidBody.position + direction * stats.MovementSpeed * Time.fixedDeltaTime
                );
                CheckDistanceToTank();
            }
        }

        private Vector2 GetDirectionToTank()
        {
            return (tank.transform.position - transform.position).normalized;
        }

        private void RotateToTank(Vector2 direction)
        {
            float rotationAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            transform.eulerAngles = Vector3.forward * -rotationAngle;
        }

        public void TakeDamage(float damageAmount)
        {
            stats.Health -= damageAmount;
            if (stats.Health <= 0)
            {
                OnDeath?.Invoke();
            }
        }

        [Button]
        [FoldoutGroup("Modules")]
        private void RefreshModules()
        {
            Modules = new()
            {
                new MovementModule(),
                new DamageModule(),
                new ExperienceModule(),
                new HealthModule(),
                new ExplosionModule()
            };
        }

        private void CheckDistanceToTank()
        {
            Vector2 distance = transform.position - tank.transform.position;
            if (distance.magnitude <= stats.ExplosionRadius)
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
            rigidBody.gameObject.SetActive(false);
            isMoving = false;
            particle.Play();
            tank.TakeDamage(stats.Damage);
            Destroy(gameObject, particle.main.duration * particle.main.startLifetimeMultiplier);
        }
    }
}
