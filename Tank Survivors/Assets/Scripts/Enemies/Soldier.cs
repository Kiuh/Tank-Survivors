using Common;
using System.Collections;
using Tank;
using UnityEngine;

namespace Enemies
{
    public class Soldier : MovingEnemy
    {
        [SerializeField]
        private float damage;

        [SerializeField]
        private float timeForNextHit;

        [SerializeField]
        [InspectorReadOnly]
        private bool isTouchingTank;

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

        private IEnumerator DealDamage()
        {
            while (isTouchingTank)
            {
                Tank.TakeDamage(damage);
                yield return new WaitForSeconds(timeForNextHit);
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
