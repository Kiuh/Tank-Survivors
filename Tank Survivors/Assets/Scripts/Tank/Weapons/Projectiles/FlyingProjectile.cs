using Enemies;
using System.Collections;
using UnityEngine;

namespace Tank.Weapons.Projectiles
{
    public class FlyingProjectile : MonoBehaviour, IProjectile
    {
        [SerializeField]
        private float scaleModifier = 1.5f;

        private float damage;
        private float speed;
        private float size;
        private float damageRadius;
        private Vector3 startPoint;
        private Vector3 endPoint;

        public void Initialize(
            float damage,
            float speed,
            float size,
            float damageRadius,
            Vector3 endPoint
        )
        {
            this.damage = damage;
            this.speed = speed;
            this.size = size;
            transform.localScale = new Vector3(size, size, 1f);
            startPoint = transform.position;
            this.endPoint = endPoint;
        }

        public void StartFly()
        {
            _ = StartCoroutine(Fly(Time.time));
        }

        private IEnumerator Fly(float startTime)
        {
            float distance = (endPoint - startPoint).magnitude;

            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * speed / distance;

                transform.position = Vector3.Lerp(startPoint, endPoint, t);
                float scale = size + Mathf.PingPong(Time.time, (size * scaleModifier) - 1);
                transform.localScale = new Vector3(scale, scale, 1f);
                yield return null;
            }

            Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, damageRadius);
            foreach (Collider2D collision in collisions)
            {
                if (collision.transform.TryGetComponent<IEnemy>(out IEnemy enemy))
                {
                    enemy.TakeDamage(damage);
                }
            }
            Destroy(gameObject);
        }
    }
}
