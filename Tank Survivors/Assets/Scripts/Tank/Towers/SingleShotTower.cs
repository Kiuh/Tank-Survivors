using Common;
using Tank.Weapons;
using Tank.Weapons.Projectiles;
using UnityEngine;

namespace Tank.Towers
{
    public class SingleShotTower : MonoBehaviour, ITower, ICanRotate
    {
        [SerializeField]
        private Transform shotPoint;

        private float shotCooldown = 0f;
        private float rotationSpeed;
        private Quaternion targetRotation;
        private SpawnVariation spawnVariation;
        private GunBase weapon;
        private EnemyFinder enemyFinder;
        private TankImpl tank;

        public ProjectileSpawner ProjectileSpawner { get; private set; }

        private void Update()
        {
            ProceedAttack();
        }

        private void LateUpdate()
        {
            RotateInternal();
        }

        public void Initialize(
            TankImpl tank,
            EnemyFinder enemyFinder,
            GunBase weapon,
            SpawnVariation spawnVariation
        )
        {
            this.tank = tank;
            this.enemyFinder = enemyFinder;
            this.weapon = weapon;
            this.spawnVariation = spawnVariation;
            ProjectileSpawner = new(weapon, this);
        }

        public void ProceedAttack()
        {
            Transform nearestEnemy = enemyFinder.GetNearestTransformOrNull();
            if (nearestEnemy == null)
            {
                return;
            }

            RotateTo(
                new RotationParameters()
                {
                    Direction = tank.transform.position - nearestEnemy.position,
                    Speed = weapon.GetModule<TowerRotationModule>().RotationSpeed.GetModifiedValue()
                }
            );

            shotCooldown -= Time.deltaTime;
            if (shotCooldown < 0f)
            {
                shotCooldown += weapon
                    .GetModule<FireRateModule>()
                    .FireRate.GetPercentagesValue(tank.FireRateModifier);

                FireAllProjectiles();
            }
        }

        private void FireAllProjectiles()
        {
            int projectileCount = weapon
                .GetModule<ProjectilesPerShootModule>()
                .ProjectilesPerShoot.GetModifiedValue();

            for (int i = 0; i < projectileCount; i++)
            {
                // var projectile = GetProjectile< weapon.GetModule<ProjectileModule<FlyingProjectile>>().ProjectilePrefab.GetType() >;
            }
        }

        public Vector3 GetShotPoint()
        {
            return shotPoint.position;
        }

        public Vector3 GetDirection()
        {
            return transform.up;
        }

        public void RotateTo(RotationParameters parameters)
        {
            targetRotation = Quaternion.LookRotation(Vector3.forward, parameters.Direction);
            rotationSpeed = parameters.Speed;
        }

        private void RotateInternal()
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                Time.deltaTime * rotationSpeed
            );
        }

        public void ChangeSpawnVariation(SpawnVariation newSpawnVariation)
        {
            spawnVariation = newSpawnVariation;
        }

        public IProjectile GetProjectile()
        {
            return ProjectileSpawner.Spawn(spawnVariation, transform);
        }
    }
}
