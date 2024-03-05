using Tank.Weapons;
using Tank.Weapons.Projectiles;
using UnityEngine;

namespace Tank.Towers
{
    public class SingleShotTower : MonoBehaviour, ITower, ICanRotate
    {
        [SerializeField]
        private Transform shotPoint;

        private float rotationSpeed;
        private Quaternion targetRotation;
        private SpawnVariation spawnVariation;
        private GunBase weapon;

        public ProjectileSpawner ProjectileSpawner { get; private set; }

        private void LateUpdate()
        {
            RotateInternal();
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

        public void Initialize(GunBase weapon, SpawnVariation spawnVariation)
        {
            this.weapon = weapon;
            this.spawnVariation = spawnVariation;
            ProjectileSpawner = new(weapon, this);
        }

        public T GetProjectile<T>()
            where T : MonoBehaviour, IProjectile
        {
            return ProjectileSpawner.Spawn<T>(spawnVariation, transform);
        }
    }
}
