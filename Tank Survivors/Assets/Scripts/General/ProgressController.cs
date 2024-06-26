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
            progress++;
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
