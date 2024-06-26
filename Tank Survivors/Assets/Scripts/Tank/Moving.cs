using Sirenix.OdinInspector;
using UnityEngine;

namespace Tank
{
    [AddComponentMenu("Tank.Moving")]
    public class Moving : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private TankImpl tank;

        [Required]
        [SerializeField]
        private Rigidbody2D tankRigidBody;

        [SerializeField]
        [ReadOnly]
        private Vector2 movementDirection;
        public Vector2 MovementDirection
        {
            get => movementDirection;
            set => movementDirection = value;
        }

        public void FixedUpdate()
        {
            Move(tank.Speed.GetModifiedValue());
        }

        private void Move(float movementSpeed)
        {
            tankRigidBody.MovePosition(
                tankRigidBody.position + (movementSpeed * Time.fixedDeltaTime * MovementDirection)
            );
            if (MovementDirection != Vector2.zero)
            {
                float lookRotationAngle =
                    Mathf.Atan2(MovementDirection.x, MovementDirection.y) * Mathf.Rad2Deg;
                transform.eulerAngles = Vector3.forward * -lookRotationAngle;
            }
        }
    }
}
