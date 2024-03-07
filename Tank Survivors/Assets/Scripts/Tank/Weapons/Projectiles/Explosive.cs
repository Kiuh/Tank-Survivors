using System.Collections;
using Enemies;
using UnityEngine;

namespace Tank.Weapons.Projectiles
{
    public class Explosive
    {
        private Transform hitMark;
        private ParticleSystem explosionParticle;
        private ParticleSystem fireParticle;

        private GameObject projectile;
        private float explosionDamage;
        private float damageRadius;

        private FireParameters fireParameters;

        public Explosive(
            Transform hitMark,
            ParticleSystem explosionParticle,
            ParticleSystem fireParticle,
            GameObject projectile,
            float explosionDamage,
            float damageRadius,
            FireParameters fireParameters
        )
        {
            this.hitMark = hitMark;
            this.explosionParticle = explosionParticle;
            this.fireParticle = fireParticle;
            this.projectile = projectile;
            this.explosionDamage = explosionDamage;
            this.damageRadius = damageRadius;
            this.fireParameters = fireParameters;
        }

        public IEnumerator Explode(float timeBeforeExplode)
        {
            yield return new WaitForSeconds(timeBeforeExplode);
            yield return Explode();
        }

        public IEnumerator Explode()
        {
            GameObject.Destroy(hitMark.gameObject);

            DamageByExplode();

            yield return Burn();

            GameObject.Destroy(
                projectile,
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
            Collider2D[] collisions = Physics2D.OverlapCircleAll(
                projectile.transform.position,
                damageRadius
            );
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
