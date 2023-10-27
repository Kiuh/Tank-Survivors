using UnityEngine;

namespace Tank
{
    [AddComponentMenu("Tank.Moving")]
    public class Moving : MonoBehaviour
    {
        [SerializeField]
        private TankImpl tank;
        private float movementSpeed;
        private Rigidbody2D tankRigidBody;
        public Vector2 MovementDirection { get; set; }

        public void Start()
        {
            tankRigidBody = tank.GetComponent<Rigidbody2D>();
            movementSpeed = tank.Speed.SourceValue;
        }

        public void FixedUpdate()
        {
            if (MovementDirection == Vector2.zero)
            {
                return;
            }

            Move();
        }

        private void Move()
        {
            tankRigidBody.MovePosition(
                tankRigidBody.position + (movementSpeed * Time.fixedDeltaTime * MovementDirection)
            );
            float angle = Mathf.Atan2(MovementDirection.x, MovementDirection.y) * Mathf.Rad2Deg;
            transform.eulerAngles = Vector3.forward * -angle;
        }
    }
}
