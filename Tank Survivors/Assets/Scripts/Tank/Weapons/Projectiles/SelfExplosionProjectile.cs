using System.Collections;
using Enemies;
using UnityEngine;

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

        private float fireDamage;
        private float fireTime;
        private float fireFireRate;

        public void Initialize(
            float explosionDamage,
            float damageRadius,
            float fireDamage,
            float fireTime,
            float fireFireRate
        )
        {
            this.explosionDamage = explosionDamage;
            this.damageRadius = damageRadius;

            this.fireDamage = fireDamage;
            this.fireTime = fireTime;
            this.fireFireRate = fireFireRate;

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
            if (fireTime <= 0)
            {
                yield break;
            }

            fireParticle.Play();

            var fireTimer = fireTime;
            while (fireTimer >= 0)
            {
                yield return new WaitForSeconds(fireFireRate);
                DamageByFire();

                fireTimer -= fireFireRate;
                Debug.Log(fireTimer);
            }
        }

        private void DamageByFire()
        {
            DealDamage(fireDamage);
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
