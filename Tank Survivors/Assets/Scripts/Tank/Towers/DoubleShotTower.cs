using System;
using Common;
using Tank.Weapons;
using Tank.Weapons.Modules;
using Tank.Weapons.Projectiles;
using UnityEngine;

namespace Tank.Towers
{
    public class DoubleShotTower : MonoBehaviour, ITower, ICanRotate
    {
        [SerializeField]
        private Transform[] shotPoints = new Transform[2];

        private float rotationSpeed;
        private Quaternion targetRotation;

        private int currentShotPoint = 0;
        private SpawnVariation spawnVariation;
        private GunBase weapon;
        private TankImpl tank;
        private EnemyFinder enemyFinder;

        private float remainingTime = 0f;

        public event Action OnProceedAttack;

        private void LateUpdate()
        {
            RotateInternal();
        }

        private void OnDestroy()
        {
            ClearReference();
        }

        public Vector3 GetShotPoint()
        {
            currentShotPoint = (currentShotPoint + 1) % shotPoints.Length;

            return shotPoints[currentShotPoint].position;
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
        }

        public void ProceedAttack()
        {
            Transform nearestEnemy = enemyFinder.GetNearestTransformOrNull();
            if (nearestEnemy == null)
            {
                return;
            }

            OnProceedAttack?.Invoke();

            RotateTo(
                new RotationParameters()
                {
                    Direction = nearestEnemy.position - tank.transform.position,
                    Speed = weapon.GetModule<TowerRotationModule>().RotationSpeed.GetModifiedValue()
                }
            );

            remainingTime -= Time.deltaTime;
            if (remainingTime < 0f)
            {
                remainingTime += weapon
                    .GetModule<FireRateModule>()
                    .FireRate.GetPercentagesValue(tank.FireRateModifier);
                FireAllProjectiles();
            }
        }

        public ITower Spawn(Transform transform)
        {
            return Instantiate(this, transform);
        }

        public void DestroyYourself()
        {
            Destroy(gameObject);
        }

        private void ClearReference()
        {
            weapon.GetModule<TowerModule>().Tower = null;
        }

        private void FireAllProjectiles()
        {
            int projectileCount = weapon
                .GetModule<ProjectilesPerShootModule>()
                .ProjectilesPerShoot.GetModifiedValue();

            for (int i = 0; i < projectileCount; i++)
            {
                FireProjectile();
            }
        }

        private void FireProjectile()
        {
            IProjectile projectilePrefab = weapon.GetModule<ProjectileModule>().ProjectilePrefab;
            IProjectile projectile = SpawnProjectile(projectilePrefab);

            projectile.Initialize(weapon, tank, this, GetShotPoint(), GetDirection());
        }

        private IProjectile SpawnProjectile(IProjectile projectilePrefab)
        {
            if (spawnVariation == SpawnVariation.Disconnected)
            {
                return projectilePrefab.Spawn();
            }
            else
            {
                return projectilePrefab.SpawnConnected(transform);
            }
        }
    }
}
