using Common;
using System;
using System.Collections;
using Tank;
using UnityEngine;

namespace Enemies
{
    public abstract class MovingEnemy : MonoBehaviour, IEnemy
    {
        [SerializeField]
        [InspectorReadOnly]
        private TankImpl tank;
        public TankImpl Tank => tank;

        private Configs.MovingEnemy movingEnemyConfig;
        public Configs.MovingEnemy MovingEnemyConfig
        {
            set => movingEnemyConfig = value;
        }

        [SerializeField]
        private Rigidbody2D enemyRigidBody;
        public Rigidbody2D EnemyRigidBody => enemyRigidBody;

        [SerializeField]
        [InspectorReadOnly]
        private float health;
        public float Health => health;

        [SerializeField]
        [InspectorReadOnly]
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

        [SerializeField]
        [InspectorReadOnly]
        private bool isMoving;

        public event Action OnDeath;

        public void Initialize(TankImpl tank)
        {
            movementSpeed = movingEnemyConfig.MovementSpeed;
            health = movingEnemyConfig.Health;
            this.tank = tank;
            StartMovement();
        }

        public void StartMovement()
        {
            isMoving = true;
            _ = StartCoroutine(Movement());
        }

        public void StopMovement()
        {
            isMoving = false;
            StopCoroutine(Movement());
        }

        private IEnumerator Movement()
        {
            while (isMoving)
            {
                Move();
                yield return new WaitForFixedUpdate();
            }
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
