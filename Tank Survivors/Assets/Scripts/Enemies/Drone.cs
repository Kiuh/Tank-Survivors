using System;
using System.Collections;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Tank;
using UnityEngine;

namespace Enemies
{
    [AddComponentMenu("Enemies.Drone")]
    public class Drone : SerializedMonoBehaviour, IEnemy
    {
        [SerializeField]
        private Configs.Drone droneConfig;

        [SerializeField]
        [ReadOnly]
        private Configs.Drone clonedConfig;

        [SerializeField]
        [ReadOnly]
        private TankImpl tank;

        [SerializeField]
        private Rigidbody2D enemyRigidBody;

        [SerializeField]
        private CircleCollider2D explosiveArea;

        [SerializeField]
        private SpriteRenderer explosiveAreaVisualization;

        [SerializeField]
        private ParticleSystem particle;

        [SerializeField]
        private SpriteRenderer sprite;

        [SerializeField]
        [ReadOnly]
        private Vector2 movementDirection;

        [SerializeField]
        [ReadOnly]
        private bool isMoving;

        [OdinSerialize]
        public string EnemyName { get; private set; }

        public event Action OnDeath;

        public void Initialize(TankImpl tank)
        {
            this.tank = tank;

            clonedConfig = Instantiate(droneConfig);
            explosiveArea.radius = clonedConfig.ExplosionRadius;

            explosiveAreaVisualization.transform.localScale =
                2.0f * clonedConfig.ExplosionRadius * Vector3.one;
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

        private IEnumerator Move()
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
                StopMovement();

                particle.transform.localScale = new Vector3(
                    clonedConfig.ExplosionRadius,
                    clonedConfig.ExplosionRadius,
                    1.0f
                );

                tank.TakeDamage(clonedConfig.Damage);
                particle.Play();
                sprite.enabled = false;
                explosiveArea.enabled = false;

                Destroy(gameObject, particle.main.duration * particle.main.startLifetimeMultiplier);
            }
        }
    }
}
