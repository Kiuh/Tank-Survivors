using Common;
using System;
using System.Collections;
using Tank;
using UnityEngine;

namespace Enemies
{
    [AddComponentMenu("Enemies.Drone")]
    public class Drone : MonoBehaviour, IEnemy
    {
        [SerializeField]
        private Configs.Drone droneConfig;

        [SerializeField]
        [InspectorReadOnly]
        private float health;

        [SerializeField]
        [InspectorReadOnly]
        private float damage;

        [SerializeField]
        [InspectorReadOnly]
        private float movementSpeed;

        [SerializeField]
        [InspectorReadOnly]
        private float explosionRadius;

        [SerializeField]
        [InspectorReadOnly]
        private float timeToExplode;

        [SerializeField]
        [InspectorReadOnly]
        private TankImpl tank;

        [SerializeField]
        private Rigidbody2D enemyRigidBody;

        [SerializeField]
        private CircleCollider2D explosiveArea;

        [SerializeField]
        [InspectorReadOnly]
        private Vector2 movementDirection;

        [SerializeField]
        [InspectorReadOnly]
        private bool isGonnaExplode = false;
        private bool isTankClose;

        [SerializeField]
        [InspectorReadOnly]
        private bool isMoving;

        public event Action OnDeath;

        public void Initialize(TankImpl tank)
        {
            health = droneConfig.Health;
            movementSpeed = droneConfig.MovementSpeed;
            damage = droneConfig.Damage;
            explosionRadius = droneConfig.ExplosionRadius;
            timeToExplode = droneConfig.TimeToExplode;
            explosiveArea.radius = explosionRadius;
            this.tank = tank;
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

        private IEnumerator Explode()
        {
            StopMovement();
            enemyRigidBody.isKinematic = true;
            yield return new WaitForSeconds(timeToExplode);
            if (isTankClose)
            {
                tank.TakeDamage(damage);
            }
            OnDeath?.Invoke();
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
                isTankClose = true;
                if (isGonnaExplode)
                {
                    return;
                }
                isGonnaExplode = true;
                _ = StartCoroutine(Explode());
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out TankImpl _))
            {
                isTankClose = false;
            }
        }
    }
}
