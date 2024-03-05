using System.Collections;
using Enemies;
using UnityEngine;

public struct FireParameters
{
    public float Damage;
    public float Time;
    public float FireRate;
}

namespace Tank.Weapons.Projectiles
{
    public class SelfExplosionProjectile : MonoBehaviour, IProjectile
    {
        [SerializeField]
        private Transform hitMark;

        [SerializeField]
        private ParticleSystem explosionParticle;

        [SerializeField]
        private ParticleSystem fireParticle;

        private float explosionDamage;
        private float damageRadius;

        private FireParameters fireParameters;

        public void Initialize(
            float explosionDamage,
            float damageRadius,
            FireParameters fireParameters
        )
        {
            this.explosionDamage = explosionDamage;
            this.damageRadius = damageRadius;
            this.fireParameters = fireParameters;

            var explosionSize = new Vector3(damageRadius, damageRadius, damageRadius);

            hitMark.localScale = explosionSize;
            explosionParticle.transform.localScale = explosionSize;
            fireParticle.transform.localScale = explosionSize;
        }

        public void StartExplosion(float timeBeforeExplode)
        {
            _ = StartCoroutine(Explode(timeBeforeExplode));
        }

        private IEnumerator Explode(float timeBeforeExplode)
        {
            yield return new WaitForSeconds(timeBeforeExplode);
            Destroy(hitMark.gameObject);

            DamageByExplode();

            yield return Burn();

            Destroy(
                gameObject,
                explosionParticle.main.duration * explosionParticle.main.startLifetimeMultiplier
            );
        }

        private void DamageByExplode()
        {
            explosionParticle.Play();
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
    }
}
