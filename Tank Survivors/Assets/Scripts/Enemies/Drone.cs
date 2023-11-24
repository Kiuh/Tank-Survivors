using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections;
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
        private float health;

        [SerializeField]
        [ReadOnly]
        private float damage;

        [SerializeField]
        [ReadOnly]
        private float movementSpeed;

        [SerializeField]
        [ReadOnly]
        private float explosionRadius;

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
            health = droneConfig.Health;
            damage = droneConfig.Damage;
            explosionRadius = droneConfig.ExplosionRadius;
            explosiveArea.radius = explosionRadius;
            explosiveAreaVisualization.transform.localScale = 2.0f * explosionRadius * Vector3.one;
            movementSpeed = droneConfig.MovementSpeed;
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
            movementDirection = direction.normalized * movementSpeed;
        }

        private void RotateToTank()
        {
            float rotationAngle =
                Mathf.Atan2(movementDirection.x, movementDirection.y) * Mathf.Rad2Deg;
            transform.eulerAngles = Vector3.forward * -rotationAngle;
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
                StopMovement();
                particle.transform.localScale = new Vector3(explosionRadius, explosionRadius, 1.0f);
                particle.Play();
                sprite.enabled = false;
                explosiveArea.enabled = false;
                tank.TakeDamage(damage);
                Destroy(gameObject, particle.main.duration * particle.main.startLifetimeMultiplier);
            }
        }
    }
}
