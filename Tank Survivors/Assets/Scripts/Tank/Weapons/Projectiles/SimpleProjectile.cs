using Common;
using Enemies;
using Tank.Towers;
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

        public void Initialize(GunBase weapon, TankImpl tank, ITower tower)
        {
            var towerDirection = tower.GetDirection();
            Vector3 spreadDirection = weapon.GetSpreadDirection(
                towerDirection,
                weapon.GetModule<ProjectileSpreadAngleModule>().SpreadAngle.GetModifiedValue()
            );

            float damage = weapon.GetModifiedDamage(
                weapon.GetModule<DamageModule>().Damage,
                weapon.GetModule<CriticalChanceModule>().CriticalChance,
                weapon.GetModule<CriticalMultiplierModule>().CriticalMultiplier,
                tank
            );

            InitializeInternal(
                damage,
                weapon.GetModule<ProjectileSpeedModule>().ProjectileSpeed.GetModifiedValue(),
                weapon
                    .GetModule<ProjectileSizeModule>()
                    .ProjectileSize.GetPercentagesValue(tank.ProjectileSize),
                weapon
                    .GetModule<FireRangeModule>()
                    .FireRange.GetPercentagesValue(tank.RangeModifier),
                weapon.GetModule<PenetrationModule>().Penetration.GetModifiedValue(),
                spreadDirection
            );
        }

        private void InitializeInternal(
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

        public void Shoot() { }
    }
}
