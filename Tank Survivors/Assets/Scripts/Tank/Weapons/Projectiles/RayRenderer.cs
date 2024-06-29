using System.Collections;
using Common;
using Enemies;
using Sirenix.OdinInspector;
using Tank.Weapons.Modules;
using UnityEngine;

namespace Tank.Weapons.Projectiles
{
    [RequireComponent(typeof(LineRenderer))]
    public class RayRenderer : MonoBehaviour, IProjectile
    {
        [Required]
        [SerializeField]
        private LineRenderer lineRenderer;

        private Damage damage;

        private float duration = 1f;
        private float timeRemaining;
        private float shotCooldown;

        private Color startColor;
        private Color endColor;

        private Vector3 startPoint;
        private Vector3 endPoint;

        public void Initialize(GunBase weapon, TankImpl tank, Vector3 shotPoint, Vector3 direction)
        {
            float fireRange = weapon
                .GetModule<FireRangeModule>()
                .FireRange.GetPercentagesValue(tank.RangeModifier);

            Damage damage = weapon.GetModifiedDamage(
                weapon.GetModule<Modules.DamageModule>().Damage,
                weapon.GetModule<CriticalChanceModule>().CriticalChance,
                weapon.GetModule<CriticalMultiplierModule>().CriticalMultiplier,
                tank
            );

            Vector3 localShotPoint = transform.InverseTransformPoint(shotPoint);
            Vector3 localDirection = Quaternion.Inverse(transform.rotation) * direction;

            InitializeInternal(
                damage,
                weapon.GetModule<RayDurationModule>().RayDuration.GetModifiedValue(),
                weapon.GetModule<RayFireRateModule>().FireRate.GetModifiedValue(),
                localShotPoint,
                localShotPoint + (localDirection.normalized * fireRange)
            );
        }

        public void Shoot()
        {
            _ = StartCoroutine(Disappear());
        }

        public IProjectile Spawn()
        {
            return Instantiate(this);
        }

        public IProjectile SpawnConnected(Transform parent)
        {
            return Instantiate(this, parent);
        }

        private void InitializeInternal(
            Damage damage,
            float duration,
            float shotCooldown,
            Vector3 startPoint,
            Vector3 endPoint
        )
        {
            this.damage = damage;

            this.duration = duration;
            timeRemaining = duration;
            this.shotCooldown = shotCooldown;

            this.startPoint = startPoint;
            this.endPoint = endPoint;

            lineRenderer.SetPosition(0, startPoint);
            lineRenderer.SetPosition(1, endPoint);

            startColor = lineRenderer.startColor;
            endColor = lineRenderer.endColor;
        }

        private IEnumerator Disappear()
        {
            lineRenderer.enabled = true;

            float distance = (endPoint - startPoint).magnitude;
            float shotCooldown = 0f;

            while (timeRemaining > 0f)
            {
                if (shotCooldown <= 0)
                {
                    shotCooldown = this.shotCooldown;

                    Vector3 globalStartPoint = transform.TransformPoint(startPoint);
                    Vector3 globalEndPoint = transform.TransformPoint(endPoint);
                    Vector3 direction = globalEndPoint - globalStartPoint;

                    SetAlpha(timeRemaining / duration);

                    RaycastHit2D[] collisions = Physics2D.RaycastAll(
                        globalStartPoint,
                        direction,
                        distance
                    );

                    foreach (RaycastHit2D collision in collisions)
                    {
                        if (collision.transform.TryGetComponent(out IEnemy enemy))
                        {
                            enemy.TakeDamage(damage);
                        }
                    }
                }

                yield return null;
                timeRemaining -= Time.deltaTime;
                shotCooldown -= Time.deltaTime;
            }

            SetAlpha(0f);
            yield return null;

            lineRenderer.enabled = false;
            Destroy(gameObject);
        }

        private void SetAlpha(float a)
        {
            startColor.a = a;
            lineRenderer.startColor = startColor;
            endColor.a = a;
            lineRenderer.endColor = endColor;
        }
    }
}
