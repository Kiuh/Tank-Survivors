using System.Collections;
using Enemies;
using UnityEngine;

namespace Tank.Weapons.Projectiles
{
    public class FlyingProjectile : MonoBehaviour, IProjectile
    {
        [SerializeField]
        private Transform hitMarkPrefab;

        [SerializeField]
        private ParticleSystem particles;

        [SerializeField]
        private SpriteRenderer sprite;

        [SerializeField]
        private float scaleModifier = 0.5f;

        private float damage;
        private float speed;
        private float size;
        private float damageRadius;
        private Vector3 startPoint;
        private Vector3 endPoint;

        private Transform hitMark;

        public void Initialize(
            float damage,
            float speed,
            float size,
            float damageRadius,
            Vector3 direction
        )
        {
            this.damage = damage;
            this.speed = speed;
            this.size = size;
            this.damageRadius = damageRadius;
            transform.localScale = new Vector3(size, size, 1f);
            startPoint = transform.position;
            endPoint = startPoint + direction;

            hitMark = Instantiate(hitMarkPrefab, startPoint + direction, Quaternion.identity);
            hitMark.localScale = new Vector3(damageRadius, damageRadius, 1f);
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
                float scale = GetScale(t);
                transform.localScale = new Vector3(scale, scale, 1f);
                yield return null;
            }

            Explode();
        }

        private void Explode()
        {
            Destroy(hitMark.gameObject);

            particles.transform.localScale = new Vector3(damageRadius, damageRadius, 1f);
            particles.Play();
            sprite.enabled = false;

            Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, damageRadius);
            foreach (Collider2D collision in collisions)
            {
                if (collision.transform.TryGetComponent<IEnemy>(out IEnemy enemy))
                {
                    enemy.TakeDamage(damage);
                }
            }
            Destroy(gameObject, particles.main.duration * particles.main.startLifetimeMultiplier);
        }

        private float GetScale(float t)
        {
            return size + 2f * scaleModifier * (t < 0.5f ? t : (1 - t));
        }
    }
}
