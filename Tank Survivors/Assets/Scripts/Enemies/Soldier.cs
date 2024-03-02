using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using Configs;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Tank;
using UnityEngine;

namespace Enemies
{
    [AddComponentMenu("Enemies.Soldier")]
    public class Soldier : SerializedMonoBehaviour, IEnemy
    {
        public string EnemyName { get; private set; }

        private SoliderConfig stats;

        [OdinSerialize]
        [ListDrawerSettings(
            HideAddButton = true,
            HideRemoveButton = true,
            AlwaysAddDefaultValue = true,
            DraggableItems = false
        )]
        [FoldoutGroup("Modules")]
        public List<IModule> Modules { get; set; }

        public event Action OnDeath;

        private TankImpl tank;
        private bool touchingEnemy = false;

        public void Initialize(TankImpl tank)
        {
            this.tank = tank;
            stats.Health = Modules.GetConcrete<HealthModule, IModule>().Health.GetModifiedValue();
            stats.Damage = Modules.GetConcrete<DamageModule, IModule>().Damage.GetModifiedValue();
            stats.ExperienceDropAmount = Modules
                .GetConcrete<ExperienceModule, IModule>()
                .DropAmount.GetModifiedValue();
            stats.TimeForNextHit = Modules
                .GetConcrete<AttackCooldownModule, IModule>()
                .Cooldown.GetModifiedValue();
            stats.MovementSpeed = Modules
                .GetConcrete<MovementModule, IModule>()
                .Speed.GetModifiedValue();
            OnDeath += () => tank.EnemyPickupsGenerator.GeneratePickup(this, transform);
            OnDeath += () => Destroy(gameObject);
        }

        private void FixedUpdate()
        {
            Vector3 direction = GetDirectionToTank();
            RotateToTank(direction);
            transform.position += direction * stats.MovementSpeed * Time.fixedDeltaTime;
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
                new AttackCooldownModule(),
                new ExperienceModule(),
                new HealthModule()
            };
        }

        private IEnumerator DealDamage()
        {
            while (touchingEnemy)
            {
                tank.TakeDamage(stats.Damage);
                yield return new WaitForSeconds(stats.TimeForNextHit);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<TankImpl>(out _))
            {
                touchingEnemy = true;
                _ = StartCoroutine(DealDamage());
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<TankImpl>(out _))
            {
                touchingEnemy = false;
            }
        }
    }
}
