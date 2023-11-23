using Common;
using System;
using System.Collections;
using Tank;
using Tank.PickUps;
using UnityEngine;
using UnityEngine.UI;

namespace Enemies
{
    [AddComponentMenu("Enemies.Soldier")]
    public class Soldier : MonoBehaviour, IEnemy
    {
        [SerializeField]
        private Configs.Soldier soldierConfig;

        [SerializeField]
        private ExperiencePickUp experiencePickupPrefab;

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
        private float timeForNextHit;

        [SerializeField]
        [InspectorReadOnly]
        private TankImpl tank;

        [SerializeField]
        private Rigidbody2D enemyRigidBody;

        [SerializeField]
        [InspectorReadOnly]
        private Vector2 movementDirection;

        [SerializeField]
        [InspectorReadOnly]
        private bool isTouchingTank;

        [SerializeField]
        [InspectorReadOnly]
        private bool isMoving;

        public event Action OnDeath;

        public void Initialize(TankImpl tank)
        {
            this.tank = tank;
            health = soldierConfig.Health;
            damage = soldierConfig.Damage;
            movementSpeed = soldierConfig.MovementSpeed;
            timeForNextHit = soldierConfig.TimeForNextHit;
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
            movementDirection = direction.normalized * movementSpeed;
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
                tank.TakeDamage(damage);
                yield return new WaitForSeconds(timeForNextHit);
            }
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
                .Initialize(soldierConfig.ExperienceDropAmount);
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
