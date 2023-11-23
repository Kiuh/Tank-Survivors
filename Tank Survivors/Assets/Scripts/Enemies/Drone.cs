using Sirenix.OdinInspector;
using System;
using System.Collections;
using Tank;
using Tank.PickUps;
using UnityEngine;

namespace Enemies
{
    [AddComponentMenu("Enemies.Drone")]
    public class Drone : MonoBehaviour, IEnemy
    {
        [SerializeField]
        private Configs.Drone droneConfig;

        [SerializeField]
        private ExperiencePickUp experiencePickupPrefab;

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
        [ReadOnly]
        private Vector2 movementDirection;
        
        [SerializeField]
        [ReadOnly]
        private bool isMoving;

        public event Action OnDeath;

        public void Initialize(TankImpl tank)
        {
            this.tank = tank;
            health = droneConfig.Health;
            damage = droneConfig.Damage;
            explosiveArea.radius = explosionRadius;
            movementSpeed = droneConfig.MovementSpeed;
            explosionRadius = droneConfig.ExplosionRadius;
            OnDeath += DropExperience;
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

        private void DropExperience()
        {
            Instantiate(experiencePickupPrefab, transform.position, Quaternion.identity)
                .GetComponent<ExperiencePickUp>()
                .Initialize(droneConfig.ExperienceDropAmount);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out TankImpl _))
            {
                tank.TakeDamage(damage);
            }
        }
    }
}
