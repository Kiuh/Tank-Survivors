using System;
using Sirenix.OdinInspector;
using Tank;
using UnityEngine;

namespace General
{
    public class ProgressController : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private TankImpl tank;

        [Required]
        [SerializeField]
        private EnemyGenerator enemyGenerator;

        [Required]
        [SerializeField]
        private Configs.Levels levels;

        [Required]
        [SerializeField]
        private GameContext gameContext;

        [SerializeField]
        private int bossCount = 3;
        public int BossCount => bossCount;

        [SerializeField]
        private int progress = 0;

        public int Progress => progress;
        public UpdateLevelData UpdateLevelData =>
            new() { Level = gameContext.DataTransfer.LevelInfo.Name, NewScore = progress };
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
        }

        private void OnBossDead()
        {
            progress++;
            SaveSystem.UpdateLevelsData(UpdateLevelData);
            if (Progress == BossCount)
            {
                OnWin?.Invoke();
            }
        }
    }
}
