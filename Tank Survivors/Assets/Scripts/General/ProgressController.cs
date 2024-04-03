using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace General
{
    public class ProgressController : MonoBehaviour
    {
        [SerializeField]
        EnemyGenerator enemyGenerator;

        [SerializeField]
        [ReadOnly]
        public int BossCount { get; } = 1;

        public int Progress { get; private set; } = 0;

        public event Action OnWin;

        private void Start()
        {
            enemyGenerator.OnBossDead += OnBossDead;
        }

        private void OnDestroy()
        {
            enemyGenerator.OnBossDead -= OnBossDead;
        }

        private void OnBossDead()
        {
            Progress++;
            if (Progress == BossCount)
            {
                OnWin?.Invoke();
            }
        }
    }
}
