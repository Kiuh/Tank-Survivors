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

        private Explosive explosive;

        public void StartExplosion(float timeBeforeExplode)
        {
            _ = StartCoroutine(explosive.Explode(timeBeforeExplode));
        }

        public void Initialize(
            float explosionDamage,
            float damageRadius,
            FireParameters fireParameters
        )
        {
            explosive = new Explosive(
                hitMark,
                explosionParticle,
                fireParticle,
                gameObject,
                explosionDamage,
                damageRadius,
                fireParameters
            );

            var explosionSize = new Vector3(damageRadius, damageRadius, damageRadius);

            hitMark.localScale = explosionSize;
            explosionParticle.transform.localScale = explosionSize;
            fireParticle.transform.localScale = explosionSize;
        }
    }
}
