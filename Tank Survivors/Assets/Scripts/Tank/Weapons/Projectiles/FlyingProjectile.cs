using System.Collections;
using Common;
using Enemies;
using Tank.Towers;
using UnityEngine;

namespace Tank.Weapons.Projectiles
{
    public class FlyingProjectile : MonoBehaviour, IProjectile
    {
        [SerializeField]
        private Transform hitMarkPrefab;

        [SerializeField]
        private ParticleSystem explosionParticle;

        [SerializeField]
        private ParticleSystem fireParticle;

        [SerializeField]
        private SpriteRenderer sprite;

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
        private ITower tower;

        public void Initialize(GunBase weapon, TankImpl tank, ITower tower)
        {
            this.tower = tower;

            float damage = weapon.GetModifiedDamage(
                weapon.GetModule<DamageModule>().Damage,
                weapon.GetModule<CriticalChanceModule>().CriticalChance,
                weapon.GetModule<CriticalMultiplierModule>().CriticalMultiplier,
                tank
            );

            var towerDirection = tower.GetDirection();
            Vector3 spreadDirection =
                weapon.GetSpreadDirection(
                    towerDirection,
                    weapon.GetModule<ProjectileSpreadAngleModule>().SpreadAngle.GetModifiedValue()
                )
                * weapon
                    .GetModule<FireRangeModule>()
                    .FireRange.GetPercentagesValue(tank.RangeModifier);

            FireParameters fireParameters = GetFireParameters(weapon, tank);

            InitializeInternal(
                damage,
                weapon.GetModule<ProjectileSpeedModule>().ProjectileSpeed.GetModifiedValue(),
                weapon
                    .GetModule<ProjectileSizeModule>()
                    .ProjectileSize.GetPercentagesValue(tank.ProjectileSize),
                weapon.GetModule<ProjectileDamageRadiusModule>().DamageRadius.GetModifiedValue(),
                spreadDirection,
                fireParameters
            );
        }

        private void InitializeInternal(
            float explosionDamage,
            float speed,
            float size,
            float damageRadius,
            Vector3 direction,
            FireParameters fireParameters
        )
        {
            transform.position = tower.GetShotPoint();
            transform.rotation = Quaternion.identity;

            this.speed = speed;
            this.size = size;
            transform.localScale = new Vector3(size, size, 1f);
            startPoint = transform.position;
            endPoint = startPoint + direction;

            var explosionSize = new Vector3(damageRadius, damageRadius, damageRadius);

            var hitMark = Instantiate(hitMarkPrefab, startPoint + direction, Quaternion.identity);
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
            if (collision.transform.TryGetComponent(out IEnemy enemy))
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
                Damage = weapon.GetModule<FireDamageModule>().Damage.GetModifiedValue(),
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
