using System.Collections;
using Common;
using Sirenix.OdinInspector;
using Tank.Weapons.Modules;
using UnityEngine;

namespace Tank.Weapons.Projectiles
{
    public class FlyingProjectile : MonoBehaviour, IProjectile
    {
        [Required]
        [SerializeField]
        private Transform hitMarkPrefab;

        [Required]
        [SerializeField]
        private ParticleSystem explosionParticle;

        [Required]
        [SerializeField]
        private ParticleSystem fireParticle;

        [Required]
        [SerializeField]
        private SpriteRenderer sprite;

        [Required]
        [SerializeField]
        private Collider2D collider2d;

        [SerializeField]
        private float scaleModifier = 0.5f;

        private float speed;
        private float size;
        private Vector3 startPoint;
        private Vector3 endPoint;

        private Coroutine flyCoroutine;
        private Explosive explosive;

        public void Initialize(GunBase weapon, TankImpl tank, Vector3 shotPoint, Vector3 direction)
        {
            Damage damage = weapon.GetModifiedDamage(
                weapon.GetModule<DamageModule>().Damage,
                weapon.GetModule<CriticalChanceModule>().CriticalChance,
                weapon.GetModule<CriticalMultiplierModule>().CriticalMultiplier,
                tank
            );

            Vector3 modifiedDirection =
                weapon.GetSpreadDirection(
                    direction,
                    weapon.GetModule<ProjectileSpreadAngleModule>().SpreadAngle.GetModifiedValue()
                )
                * weapon
                    .GetModule<FireRangeModule>()
                    .FireRange.GetPercentagesValue(tank.RangeModifier);

            FireParameters fireParameters = GetFireParameters(weapon, tank);

            transform.SetPositionAndRotation(shotPoint, Quaternion.identity);

            InitializeInternal(
                damage,
                weapon.GetModule<ProjectileSpeedModule>().ProjectileSpeed.GetModifiedValue(),
                weapon
                    .GetModule<ProjectileSizeModule>()
                    .ProjectileSize.GetPercentagesValue(tank.ProjectileSize),
                weapon.GetModule<ProjectileDamageRadiusModule>().DamageRadius.GetModifiedValue(),
                modifiedDirection,
                fireParameters
            );
        }

        private void InitializeInternal(
            Damage explosionDamage,
            float speed,
            float size,
            float damageRadius,
            Vector3 direction,
            FireParameters fireParameters
        )
        {
            this.speed = speed;
            this.size = size;
            transform.localScale = new Vector3(size, size, 1f);
            startPoint = transform.position;
            endPoint = startPoint + direction;

            Vector3 explosionSize = new(damageRadius, damageRadius, damageRadius);

            Transform hitMark = Instantiate(
                hitMarkPrefab,
                startPoint + direction,
                Quaternion.identity
            );
            hitMark.localScale = explosionSize;
            explosionParticle.transform.localScale = explosionSize;
            fireParticle.transform.localScale = explosionSize;

            explosive = new Explosive(
                hitMark,
                explosionParticle,
                fireParticle,
                gameObject,
                explosionDamage,
                damageRadius,
                fireParameters
            );
        }

        public void Shoot()
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
            if (collision.transform.TryGetComponent(out Enemies.IEnemy _))
            {
                StopCoroutine(flyCoroutine);
                StartExplosion();
            }
        }

        public void StartExplosion()
        {
            sprite.enabled = false;
            collider2d.enabled = false;
            _ = StartCoroutine(explosive.Explode());
        }

        private float GetScale(float t)
        {
            return size + (2f * scaleModifier * (t < 0.5f ? t : (1 - t)));
        }

        private FireParameters GetFireParameters(GunBase weapon, TankImpl tank)
        {
            return new FireParameters()
            {
                Damage = new Damage(weapon.GetModule<FireDamageModule>().Damage.GetModifiedValue()),
                Time = weapon.GetModule<ProjectileFireTimerModule>().Time.GetModifiedValue(),
                FireRate = weapon
                    .GetModule<FireFireRateModule>()
                    .FireRate.GetPercentagesValue(tank.FireRateModifier)
            };
        }

        public IProjectile Spawn()
        {
            return Instantiate(this);
        }

        public IProjectile SpawnConnected(Transform parent)
        {
            return Instantiate(this, parent);
        }
    }
}
