using Common;
using UnityEngine;

namespace Tank
{
    [AddComponentMenu("Tank.Moving")]
    public class Moving : MonoBehaviour
    {
        [SerializeField]
        private TankImpl tank;

        [SerializeField]
        private Rigidbody2D tankRigidBody;

        [SerializeField]
        [InspectorReadOnly]
        private Vector2 movementDirection;
        public Vector2 MovementDirection
        {
            get => movementDirection;
            set => movementDirection = value;
        }

        public void FixedUpdate()
        {
            if (MovementDirection == Vector2.zero)
            {
                return;
            }

            Move(tank.Speed.GetModifiedValue());
        }

        private void Move(float movementSpeed)
        {
            tankRigidBody.MovePosition(
                tankRigidBody.position + (movementSpeed * Time.fixedDeltaTime * MovementDirection)
            );
            float lookRotationAngle =
                Mathf.Atan2(MovementDirection.x, MovementDirection.y) * Mathf.Rad2Deg;
            transform.eulerAngles = Vector3.forward * -lookRotationAngle;
        }
    }
}
