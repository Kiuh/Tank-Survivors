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
        private ParticleSystem explosionParticles;

        [SerializeField]
        private ParticleSystem fireParticle;

        [SerializeField]
        private SpriteRenderer sprite;

        [SerializeField]
        private Collider2D collider2d;

        [SerializeField]
        private float scaleModifier = 0.5f;

        private float explosionDamage;
        private float speed;
        private float size;
        private float damageRadius;
        private Vector3 startPoint;
        private Vector3 endPoint;

        private Transform hitMark;
        private FireParameters fireParameters;

        private Coroutine flyCoroutine;

        public void Initialize(
            float damage,
            float speed,
            float size,
            float damageRadius,
            Vector3 direction,
            FireParameters fireParameters
        )
        {
            this.explosionDamage = damage;
            this.speed = speed;
            this.size = size;
            this.damageRadius = damageRadius;
            this.fireParameters = fireParameters;
            transform.localScale = new Vector3(size, size, 1f);
            startPoint = transform.position;
            endPoint = startPoint + direction;

            var explosionSize = new Vector3(damageRadius, damageRadius, damageRadius);

            hitMark = Instantiate(hitMarkPrefab, startPoint + direction, Quaternion.identity);
            hitMark.localScale = explosionSize;
            explosionParticles.transform.localScale = explosionSize;
            fireParticle.transform.localScale = explosionSize;
        }

        public void StartFly()
        {
            flyCoroutine = StartCoroutine(Fly(Time.time));
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

            StartExplosion();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.TryGetComponent(out IEnemy enemy))
            {
                StopCoroutine(flyCoroutine);
                StartExplosion();
            }
        }

        public void StartExplosion()
        {
            _ = StartCoroutine(Explode());
        }

        private IEnumerator Explode()
        {
            hitMark.gameObject.SetActive(false);
            sprite.enabled = false;
            collider2d.enabled = false;

            DamageByExplode();

            yield return Burn();

            Destroy(
                gameObject,
                explosionParticles.main.duration * explosionParticles.main.startLifetimeMultiplier
            );
        }

        private void DamageByExplode()
        {
            explosionParticles.Play();
            DealDamage(explosionDamage);
        }

        private IEnumerator Burn()
        {
            if (fireParameters.Time <= 0)
            {
                yield break;
            }

            fireParticle.Play();

            var fireTimer = fireParameters.Time;
            while (fireTimer >= 0)
            {
                yield return new WaitForSeconds(fireParameters.FireRate);
                DamageByFire();

                fireTimer -= fireParameters.FireRate;
                Debug.Log(fireTimer);
            }
        }

        private void DamageByFire()
        {
            DealDamage(fireParameters.Damage);
        }

        private void DealDamage(float damage)
        {
            Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, damageRadius);
            foreach (Collider2D collision in collisions)
            {
                if (collision.transform.TryGetComponent(out IEnemy enemy))
                {
                    enemy.TakeDamage(damage);
                }
            }
        }

        private float GetScale(float t)
        {
            return size + 2f * scaleModifier * (t < 0.5f ? t : (1 - t));
        }
    }
}
