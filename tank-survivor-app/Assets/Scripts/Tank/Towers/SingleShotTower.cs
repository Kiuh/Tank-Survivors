using System;
using Common;
using Sirenix.OdinInspector;
using Tank.Weapons;
using Tank.Weapons.Modules;
using Tank.Weapons.Projectiles;
using UnityEngine;
using UnityEngine.Events;

namespace Tank.Towers
{
    public class SingleShotTower : MonoBehaviour, ITower, ICanRotate
    {
        [Required]
        [SerializeField]
        private Transform shotPoint;

        private float shotCooldown = 0f;
        private float rotationSpeed;
        private Quaternion targetRotation;
        private SpawnVariation spawnVariation;
        private GunBase weapon;
        private EnemyFinder enemyFinder;
        private TankImpl tank;

        public event Action OnProceedAttack;

        private void LateUpdate()
        {
            RotateInternal();
        }

        private void OnDestroy()
        {
            ClearReference();
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

            RotateTo(
                new RotationParameters()
                {
                    Direction = nearestEnemy.position - tank.transform.position,
                    Speed = weapon.GetModule<TowerRotationModule>().RotationSpeed.GetModifiedValue()
                }
            );

            OnProceedAttack?.Invoke();
            shotCooldown -= Time.deltaTime;
            if (shotCooldown < 0f)
            {
                shotCooldown += weapon
                    .GetModule<FireRateModule>()
                    .FireRate.GetPercentagesValue(tank.FireRateModifier);

                FireAllProjectiles();
            }
        }

        public void ChangeSpawnVariation(SpawnVariation newSpawnVariation)
        {
            spawnVariation = newSpawnVariation;
        }

        public ITower Spawn(Transform transform)
        {
            return Instantiate(this, transform);
        }

        public void DestroyYourself()
        {
            Destroy(gameObject);
        }

        public Vector3 GetShotPoint()
        {
            return shotPoint.position;
        }

        public Vector3 GetDirection()
        {
            return shotPoint.up;
        }

        public void RotateTo(RotationParameters parameters)
        {
            targetRotation = Quaternion.LookRotation(Vector3.forward, parameters.Direction);
            rotationSpeed = parameters.Speed;
        }

        private void ClearReference()
        {
            weapon.GetModule<TowerModule>().RemoveTower();
        }

        public UnityEvent OnTowerShoot;

        private void FireAllProjectiles()
        {
            int projectileCount = weapon
                .GetModule<ProjectilesPerShootModule>()
                .ProjectilesPerShoot.GetModifiedValue();

            for (int i = 0; i < projectileCount; i++)
            {
                IProjectile projectilePrefab = weapon
                    .GetModule<ProjectileModule>()
                    .ProjectilePrefab;

                IProjectile projectile = SpawnProjectile(projectilePrefab);

                projectile.Initialize(weapon, tank, GetShotPoint(), GetDirection());
                projectile.Shoot();
                OnTowerShoot?.Invoke();
            }
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

        private void RotateInternal()
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                Time.deltaTime * rotationSpeed
            );
        }
    }
}
