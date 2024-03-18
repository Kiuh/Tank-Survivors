using Tank.Weapons;
using UnityEngine;

namespace Tank.Towers
{
    public enum SpawnVariation
    {
        Connected,
        Disconnected
    }

    public interface ITower
    {
        public void ProceedAttack();
        public Vector3 GetShotPoint();
        public Vector3 GetDirection();
        public void ChangeSpawnVariation(SpawnVariation newSpawnVariation);
        public void Initialize(
            TankImpl tank,
            EnemyFinder enemyFinder,
            GunBase weapon,
            SpawnVariation spawnVariation
        );
    }

    public interface ICanRotate
    {
        public void RotateTo(RotationParameters parameters);
    }

    public struct RotationParameters
    {
        public float Speed;
        public Vector3 Direction;
    }
}
