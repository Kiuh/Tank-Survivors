using Common;
using System.Collections;
using Tank;
using UnityEngine;

namespace Enemies
{
    public class Drone : MovingEnemy
    {
        [SerializeField]
        private Configs.Drone droneConfig;

        [SerializeField]
        [InspectorReadOnly]
        private float damage;

        [SerializeField]
        [InspectorReadOnly]
        private float explosionRadius;

        [SerializeField]
        private CircleCollider2D explosiveArea;

        [SerializeField]
        [InspectorReadOnly]
        private float timeToExplode;

        [SerializeField]
        [InspectorReadOnly]
        private bool isGonnaExplode = false;
        private bool isTankClose;

        public new void Initialize(TankImpl tank)
        {
            damage = droneConfig.Damage;
            explosionRadius = droneConfig.ExplosionRadius;
            timeToExplode = droneConfig.TimeToExplode;
            MovingEnemyConfig = droneConfig.MovingEnemyConfig;
            explosiveArea.radius = explosionRadius;
            base.Initialize(tank);
        }

        public override void Move()
        {
            CalculateDirectionToTank();
            RotateToTank();
            EnemyRigidBody.MovePosition(
                EnemyRigidBody.position + (MovementDirection * Time.fixedDeltaTime)
            );
        }

        private void CalculateDirectionToTank()
        {
            Vector2 direction = Tank.transform.position - transform.position;
            MovementDirection = direction.normalized * MovementSpeed;
        }

        private void RotateToTank()
        {
            float rotationAngle =
                Mathf.Atan2(MovementDirection.x, MovementDirection.y) * Mathf.Rad2Deg;
            transform.eulerAngles = Vector3.forward * -rotationAngle;
        }

        private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
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

        private IEnumerator Explode()
        {
            StopMovement();
            EnemyRigidBody.isKinematic = true;
            yield return new WaitForSeconds(timeToExplode);
            if (isTankClose)
            {
                Tank.TakeDamage(damage);
            }
            TakeDamage(Health);
        }
    }
}
