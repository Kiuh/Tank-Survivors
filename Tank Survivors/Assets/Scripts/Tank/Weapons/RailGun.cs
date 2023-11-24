using Common;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using Tank;
using Tank.Towers;
using Tank.Weapons;
using Tank.Weapons.Projectiles;
using UnityEngine;

namespace Assets.Scripts.Tank.Weapons
{
    [Serializable]
    public class RailGun : GunBase
    {
        [OdinSerialize]
        [ListDrawerSettings(
            HideAddButton = true,
            HideRemoveButton = true,
            AlwaysAddDefaultValue = true,
            DraggableItems = false
        )]
        [FoldoutGroup("$UpgradeName")]
        public override List<IWeaponModule> Modules { get; protected set; } =
            new()
            {
                new DamageModule(),
                new FireRateModule(),
                new CriticalChanceModule(),
                new CriticalMultiplierModule(),
                new FireRangeModule(),
                new ProjectileModule<RayRenderer>(),
                new RayDurationModule(),
                new TowerModule<SingleShotTower>(),
            };

        [SerializeField]
        private float rayDuration;
        public float RayDuration => rayDuration;

        private SingleShotTower tower;
        private TankImpl tank;
        private EnemyFinder enemyFinder;

        private float remainingTime = 0f;

        public override void ProceedAttack()
        {
            Transform nearestEnemy = enemyFinder.GetNearestTransformOrNull();
            if (nearestEnemy == null)
            {
                return;
            }

            remainingTime -= Time.deltaTime;

            if (remainingTime < 0f)
            {
                remainingTime += GetModule<FireRateModule>().FireRate.GetPrecentageValue(
                    tank.FireRateModifier
                );
                Vector3 shotDirection = nearestEnemy.position - tank.transform.position;

                tower.RotateTo(shotDirection);

                float fireRange = GetModule<FireRangeModule>().FireRange.GetPrecentageValue(
                    tank.RangeModifier
                );

                RayRenderer ray = UnityEngine.Object.Instantiate(
                    GetModule<ProjectileModule<RayRenderer>>().ProjectilePrefab
                );

                float damage = this.GetModifiedDamage(
                    GetModule<DamageModule>().Damage,
                    GetModule<CriticalChanceModule>().CriticalChance,
                    GetModule<CriticalMultiplierModule>().CriticalMultiplier,
                    tank
                );

                ray.Initialize(
                    damage,
                    GetModule<RayDurationModule>().RayDuration.GetModifiedValue(),
                    tower.GetShotPoint(),
                    tank.transform.position + (shotDirection.normalized * fireRange)
                );
                ray.Show();
            }
        }

        public override void Initialize(TankImpl tank, EnemyFinder enemyFinder)
        {
            CurrentLevel = 0;
            this.tank = tank;
            this.enemyFinder = enemyFinder;
            tower = UnityEngine.Object.Instantiate(
                GetModule<TowerModule<SingleShotTower>>().TowerPrefab,
                tank.transform
            );
        }
    }
}
