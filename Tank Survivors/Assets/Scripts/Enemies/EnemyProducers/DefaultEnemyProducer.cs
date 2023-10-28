using AYellowpaper;
using System;
using Tank;
using UnityEngine;

namespace Enemies.Producers
{
    [Serializable]
    public class DefaultEnemyProducer : IEnemyProducer
    {
        [SerializeField]
        private InterfaceReference<IEnemy, MonoBehaviour> enemyPrefab;

        [SerializeField]
        private float spawnInterval;

        [SerializeField]
        private float startCircleRadius;

        [SerializeField]
        private float endCircleRadius;

        private float timer = 0;

        public float StartTime => throw new NotImplementedException();

        public float EndTime => throw new NotImplementedException();

        public void Produce(TankImpl tank, Transform enemyRoot)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                UnityEngine.Object
                    .Instantiate(
                        enemyPrefab.UnderlyingValue.gameObject,
                        tank.transform.position + GetRandomPoint(),
                        Quaternion.identity,
                        enemyRoot
                    )
                    .GetComponent<IEnemy>()
                    .Initialize(tank);
            }
        }

        private Vector3 GetRandomPoint()
        {
            Vector2 point = UnityEngine.Random.insideUnitCircle;
            return (point * (endCircleRadius - startCircleRadius))
                + (point.normalized * startCircleRadius);
        }
    }
}
