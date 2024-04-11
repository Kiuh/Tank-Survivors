using System;
using Sirenix.OdinInspector;
using Tank;
using UnityEngine;

namespace Enemies.Projectiles
{
    [Serializable]
    public class Projectile : SerializedMonoBehaviour
    {
        private float damage;
        private float range;
        private float speed;
        private Vector3 startPosition;
        private Vector3 direction;

        public void Initialize(float damage, float range, float speed, Vector3 direction)
        {
            this.damage = damage;
            this.range = range;
            this.speed = speed;
            this.direction = direction;
        }

        public void Start()
        {
            startPosition = transform.position;
        }

        public void Update()
        {
            if ((transform.position - startPosition).magnitude > range)
            {
                Destroy(gameObject);
            }
            transform.Translate(Time.deltaTime * speed * direction);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out TankImpl tank))
            {
                tank.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
