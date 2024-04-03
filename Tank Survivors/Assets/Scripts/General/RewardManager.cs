using System.Collections.Generic;
using Tank;
using UnityEngine;
using YG;

namespace General
{
    public enum Reward
    {
        SecondLife
    }

    public class RewardManager : MonoBehaviour
    {
        [SerializeField]
        private TankImpl tank;

        private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;

        private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;

        void Rewarded(int id)
        {
            Reward reward = (Reward)id;

            switch (reward)
            {
                case Reward.SecondLife:
                    tank.Heal(tank.Health.MaxValue / 2);
                    IEnumerable<Transform> enemies = tank.EnemyFinder.GetAllEnemies();
                    foreach (Transform enemy in enemies)
                    {
                        Destroy(enemy.gameObject);
                    }
                    break;
                default:
                    Debug.LogError($"Unknown reward type {reward}");
                    break;
            }
        }
    }
}
