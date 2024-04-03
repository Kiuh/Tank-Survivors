﻿using System.Collections.Generic;
using Enemies;
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

        [SerializeField]
        private Panels.Death.Controller controller;

        private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;

        private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;

        void Rewarded(int id)
        {
            Reward reward = (Reward)id;

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
                enemy.TakeDamage(float.MaxValue);
            }

            controller.HideLosePanel();
        }
    }
}
