using Enemies;
using UnityEngine;

namespace Tank.Weapons.Projectiles
{
    public class SimpleProjectile : MonoBehaviour, IProjectile
    {
        private float damage;
        private float speed;
        private float fireRange;
        private int penetration;
        private Vector3 direction;

        private Vector3 startPosition;

        public void Initialize(
            float damage,
            float speed,
            float size,
            float fireRange,
            int penetration,
            Vector3 direction
        )
        {
            this.damage = damage;
            this.speed = speed;
            transform.localScale = new Vector3(size, size, 1);
            this.fireRange = fireRange;
            this.penetration = penetration;
            this.direction = direction.normalized;
        }

        private void Start()
        {
            startPosition = transform.position;
        }

        private void Update()
        {
            if ((transform.position - startPosition).magnitude > fireRange)
            {
                Destroy(gameObject);
            }
            transform.Translate(Time.deltaTime * speed * direction);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.TryGetComponent(out IEnemy enemy))
            {
                enemy.TakeDamage(damage);
                penetration--;
                if (penetration <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
