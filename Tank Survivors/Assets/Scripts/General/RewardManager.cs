using System.Collections.Generic;
using Enemies;
using Sirenix.OdinInspector;
using Tank;
using UnityEngine;

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

        [SerializeField]
        private Panels.Death.Controller controller;

        [Button]
        public void Rewarded(Reward id)
        {
            Reward reward = id;

            switch (reward)
            {
                case Reward.SecondLife:
                    ApplySecondLife();
                    break;
                default:
                    Debug.LogError($"Unknown reward type {reward}");
                    break;
            }
        }

        private void ApplySecondLife()
        {
            tank.Heal(tank.Health.MaxValue / 2);

            IEnumerable<IEnemy> enemies = tank.EnemyFinder.GetAllEnemies();
            foreach (IEnemy enemy in enemies)
            {
                if (enemy is Enemy)
                {
                    continue;
                }

                enemy.TakeDamage(float.MaxValue);
            }

            controller.HideSecondLifeButton();
            controller.HideLosePanel();
        }
    }
}
