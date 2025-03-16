using Sirenix.OdinInspector;
using UnityEngine;

namespace Tank
{
    [AddComponentMenu("Tank.Movement")]
    public class Movement : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private TankImpl tank;

        [Required]
        [SerializeField]
        private Rigidbody2D tankRigidBody;

        [Required]
        [SerializeField]
        private Transform rotationRoot;

        [SerializeField]
        [ReadOnly]
        private Vector2 movementDirection;
        public Vector2 MovementDirection
        {
            get => movementDirection;
            set => movementDirection = value;
        }

        [SerializeField]
        private float rotationLerpSpeed;

        public void FixedUpdate()
        {
            if (tank.IsDead)
            {
                return;
            }

            Move(tank.Speed.GetModifiedValue());
        }

        private void Update()
        {
            if (tank.IsDead)
            {
                return;
            }

            Rotate();
        }

        private void Rotate()
        {
            if (MovementDirection == Vector2.zero)
            {
                return;
            }

            float rotationAngle =
                Mathf.Atan2(MovementDirection.x, MovementDirection.y) * Mathf.Rad2Deg;
            rotationRoot.eulerAngles = Vector3.forward * -rotationAngle;
        }

        private void Move(float movementSpeed)
        {
            Vector2 shift = movementSpeed * Time.fixedDeltaTime * MovementDirection;
            Vector2 newPosition = tankRigidBody.position + shift;
            tankRigidBody.MovePosition(newPosition);
        }
    }
}
