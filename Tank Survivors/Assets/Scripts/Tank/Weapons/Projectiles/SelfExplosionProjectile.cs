using Common;
using Tank.Towers;
using Tank.Weapons.Modules;
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

        private GunBase weapon;
        private ITower tower;
        private Explosive explosive;

        public void StartExplosion(float timeBeforeExplode)
        {
            _ = StartCoroutine(explosive.Explode(timeBeforeExplode));
        }

        public void InitializeInternal(
            float explosionDamage,
            float damageRadius,
            FireParameters fireParameters
        )
        {
            transform.position = tower.GetShotPoint();
            transform.rotation = Quaternion.identity;

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

        public void Initialize(GunBase weapon, TankImpl tank, ITower tower)
        {
            this.weapon = weapon;
            this.tower = tower;

            float damage = weapon
                .GetModule<Modules.SelfExplosion.DamageModule>()
                .Damage.GetPercentagesModifiableValue(tank.DamageModifier)
                .GetModifiedValue();

            InitializeInternal(
                damage,
                weapon.GetModule<Modules.SelfExplosion.RadiusModule>().Radius.GetModifiedValue(),
                new FireParameters()
                {
                    Damage = weapon.GetModule<FireDamageModule>().Damage.GetModifiedValue(),
                    Time = weapon
                        .GetModule<Modules.SelfExplosion.FireTimerModule>()
                        .Time.GetModifiedValue(),
                    FireRate = weapon
                        .GetModule<FireFireRateModule>()
                        .FireRate.GetPercentagesValue(tank.FireRateModifier)
                }
            );
        }

        public void Shoot()
        {
            StartExplosion(
                weapon.GetModule<Modules.SelfExplosion.HitMarkTimerModule>().Time.GetModifiedValue()
            );
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
