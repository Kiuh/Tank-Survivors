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

        [Required]
        [SerializeField]
        private Transform aim;

        [SerializeField]
        private float aimModifier;

        [SerializeField]
        private float aimLerpSpeed;

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

        private void Move(float movementSpeed)
        {
            if (MovementDirection != Vector2.zero)
            {
                Vector2 shift =
                    tankRigidBody.position
                    + (movementSpeed * Time.fixedDeltaTime * MovementDirection);
                tankRigidBody.MovePosition(shift);

                float lookRotationAngle =
                    Mathf.Atan2(MovementDirection.x, MovementDirection.y) * Mathf.Rad2Deg;
                Quaternion newAngles = Quaternion.Euler(Vector3.forward * -lookRotationAngle);
                transform.rotation = Quaternion.Lerp(
                    transform.rotation,
                    newAngles,
                    Time.fixedDeltaTime * rotationLerpSpeed
                );
            }
        }

        private void Update()
        {
            if (tank.IsDead)
            {
                return;
            }

            Vector3 newAimPosition = tankRigidBody.position + (movementDirection * aimModifier);
            aim.position = Vector3.Lerp(
                aim.position,
                newAimPosition,
                Time.deltaTime * aimLerpSpeed
            );
        }
    }
}
