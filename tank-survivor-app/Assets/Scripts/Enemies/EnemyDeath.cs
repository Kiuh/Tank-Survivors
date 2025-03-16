using Sirenix.OdinInspector;
using TNRD;
using UnityEngine;

namespace Enemies
{
    [AddComponentMenu("Scripts/Enemies/Enemies.EnemyDeath")]
    internal class EnemyDeath : MonoBehaviour
    {
        [SerializeField]
        private SerializableInterface<IEnemy> enemy;
        private IEnemy Enemy => enemy.Value;

        [AssetSelector]
        [SerializeField]
        private ParticleSystem explosion;

        [SerializeField]
        private Vector3 explosionScale;

        private void Awake()
        {
            Enemy.OnDeath += OnEnemyDeath;
        }

        private void OnEnemyDeath()
        {
            ParticleSystem instance = Instantiate(
                explosion,
                Enemy.Transform.position,
                Quaternion.identity
            );
            instance.transform.localScale = explosionScale;
            instance.Play();
            GameObject.Destroy(
                instance.gameObject,
                instance.main.duration * instance.main.startLifetimeMultiplier
            );
        }
    }
}
