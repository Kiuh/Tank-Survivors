using UnityEngine;

namespace Enemies
{
    public class Soldier : MovingEnemy
    {
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
    }
}
