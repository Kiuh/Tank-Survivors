﻿using System.Collections.Generic;
using System.Linq;
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
        [Required]
        [SerializeField]
        private TankImpl tank;

        [Required]
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

            IEnumerable<IEnemy> enemies = tank
                .EnemyFinder.GetAllEnemies()
                .Select(x => x.GetComponent<IEnemy>())
                .Where(x => x != null);
            foreach (IEnemy enemy in enemies)
            {
                enemy.TakeDamage(float.MaxValue);
            }

            controller.HideSecondLifeButton();
            controller.HideLosePanel();
        }
    }
}
