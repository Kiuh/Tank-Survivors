using Common;
using System;
using Tank;
using UnityEngine;

namespace Enemies
{
    public abstract class MovingEnemy : MonoBehaviour, IEnemy
    {
        [SerializeField]
        //[InspectorReadOnly]
        private TankImpl tank;
        public TankImpl Tank => tank;

        [SerializeField]
        private Rigidbody2D enemyRigidBody;
        public Rigidbody2D EnemyRigidBody => enemyRigidBody;

        [SerializeField]
        private float health;
        public float Health => health;

        [SerializeField]
        private float movementSpeed;
        public float MovementSpeed => movementSpeed;

        [SerializeField]
        [InspectorReadOnly]
        private Vector2 movementDirection;
        public Vector2 MovementDirection
        {
            get => movementDirection;
            set => movementDirection = value;
        }

        public event Action OnDeath;

        public void Initialize(TankImpl tank)
        {
            this.tank = tank;
        }

        public void FixedUpdate()
        {
            Move();
        }

        public virtual void Move() { }

        public void TakeDamage(float damageAmount)
        {
            health -= damageAmount;
            if (health <= 0)
            {
                health = 0;
                OnDeath?.Invoke();
            }
        }
    }
}
