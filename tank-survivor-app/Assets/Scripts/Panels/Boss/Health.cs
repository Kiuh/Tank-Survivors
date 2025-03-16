using Enemies;
using General;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Panels.Boss
{
    [AddComponentMenu("Scripts/Panels/Boss/Panels.Boss.Health")]
    internal class Health : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private EnemyGenerator enemyGenerator;

        [Required]
        [SerializeField]
        private Image heath;

        [Required]
        [SerializeField]
        private TMP_Text bossLabel;

        [Required]
        [SerializeField]
        private GameObject root;

        [SerializeField]
        private float lerpSpeed;

        private IEnemy enemy;
        private float maxHealth;

        private void Awake()
        {
            enemyGenerator.OnBossAppeared += ShowBossHp;
            enemyGenerator.OnBossDead += HideBossHp;
        }

        public void ShowBossHp(IEnemy enemy)
        {
            this.enemy = enemy;
            maxHealth = enemy.Health;
            bossLabel.text = enemy.ViewEnemyName;
            root.SetActive(true);
        }

        private void Update()
        {
            if (root.activeSelf)
            {
                heath.fillAmount = Mathf.Lerp(
                    heath.fillAmount,
                    enemy.Health / maxHealth,
                    lerpSpeed * Time.deltaTime
                );
            }
        }

        private void HideBossHp()
        {
            enemy = null;
            root.SetActive(false);
        }
    }
}
