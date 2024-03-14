using Tank.Weapons;
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

        public ProjectileSpawner ProjectileSpawner { get; private set; }

        private void LateUpdate()
        {
            RotateInternal();
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
            ProjectileSpawner = new(weapon, this);
        }

        public IProjectile GetProjectile()
        {
            return ProjectileSpawner.Spawn(spawnVariation, transform);
        }

        public void ProceedAttack()
        {
            throw new System.NotImplementedException();
        }
    }
}
