using System;
using System.Collections;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Tank;
using UnityEngine;

namespace Enemies
{
    [AddComponentMenu("Enemies.Soldier")]
    public class Soldier : SerializedMonoBehaviour, IEnemy
    {
        [SerializeField]
        private Configs.Soldier config;

        [SerializeField]
        [ReadOnly]
        [InlineProperty]
        private Configs.SoliderConfig clonedConfig;

        [SerializeField]
        [ReadOnly]
        private TankImpl tank;

        [SerializeField]
        private Rigidbody2D enemyRigidBody;

        [SerializeField]
        [ReadOnly]
        private Vector2 movementDirection;

        [SerializeField]
        [ReadOnly]
        private bool isTouchingTank;

        [SerializeField]
        [ReadOnly]
        private bool isMoving;

        [OdinSerialize]
        public string EnemyName { get; private set; }

        public event Action OnDeath;

        public void Initialize(TankImpl tank)
        {
            this.tank = tank;

            clonedConfig = config.Config;

            OnDeath += () => tank.EnemyPickupsGenerator.GeneratePickup(this, transform);
            OnDeath += () => Destroy(gameObject);
            StartMovement();
        }

        public void StartMovement()
        {
            isMoving = true;
            _ = StartCoroutine(Move());
        }

        public void StopMovement()
        {
            isMoving = false;
            StopCoroutine(Move());
        }

        public IEnumerator Move()
        {
            while (isMoving)
            {
                CalculateDirectionToTank();
                RotateToTank();
                enemyRigidBody.MovePosition(
                    enemyRigidBody.position + (movementDirection * Time.fixedDeltaTime)
                );
                yield return new WaitForFixedUpdate();
            }
        }

        private void CalculateDirectionToTank()
        {
            Vector2 direction = tank.transform.position - transform.position;
            movementDirection = direction.normalized * clonedConfig.MovementSpeed;
        }

        private void RotateToTank()
        {
            float rotationAngle =
                Mathf.Atan2(movementDirection.x, movementDirection.y) * Mathf.Rad2Deg;
            transform.eulerAngles = Vector3.forward * -rotationAngle;
        }

        private IEnumerator DealDamage()
        {
            while (isTouchingTank)
            {
                tank.TakeDamage(clonedConfig.Damage);
                yield return new WaitForSeconds(clonedConfig.TimeForNextHit);
            }
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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out TankImpl _))
            {
                isTouchingTank = true;
                _ = StartCoroutine(DealDamage());
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out TankImpl _))
            {
                isTouchingTank = false;
            }
        }
    }
}
