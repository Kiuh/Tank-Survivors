using System;
using Sirenix.OdinInspector;
using Tank;
using UnityEngine;

namespace General
{
    public class ProgressController : MonoBehaviour
    {
        [SerializeField]
        private TankImpl tank;

        [SerializeField]
        private EnemyGenerator enemyGenerator;

        [SerializeField]
        private Configs.Levels levels;

        [SerializeField]
        private GameContext gameContext;

        [SerializeField]
        [ReadOnly]
        public int BossCount { get; private set; } = 3;

        public int Progress { get; private set; } = 0;

        public event Action OnWin;
        public event Action OnLoose;

        private void Start()
        {
            enemyGenerator.OnBossDead += OnBossDead;
            tank.OnDeath += OnTankDeath;
        }

        private void OnDestroy()
        {
            tank.OnDeath -= OnTankDeath;
            enemyGenerator.OnBossDead -= OnBossDead;
        }

        private void OnTankDeath()
        {
            OnLoose?.Invoke();
            SaveSystem.SaveData(levels);
        }

        private void OnBossDead()
        {
            Progress++;
            levels
                .LevelInfos.Find(x => x.Name.Equals(gameContext.DataTransfer.LevelInfo.Name))
                .Progress = Progress;

            if (Progress == BossCount)
            {
                OnWin?.Invoke();
                SaveSystem.SaveData(levels);
            }
        }
    }
}
