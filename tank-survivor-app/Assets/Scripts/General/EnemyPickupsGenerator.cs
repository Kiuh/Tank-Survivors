using System.Collections.Generic;
using System.Linq;
using Configs;
using DataStructs;
using Enemies;
using Module.ObjectPool.KeyPools;
using Sirenix.OdinInspector;
using Tank.PickUps;
using UnityEngine;

namespace General
{
    public class EnemyPickupsGenerator : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private EnemiesPickupsDrops enemiesPickups;

        [Required]
        [SerializeField]
        private Transform pickupsParent;

        [SerializeField]
        private Vector2 randomRange;

        [SerializeField]
        [AssetList(CustomFilterMethod = nameof(PickupsFilter), AutoPopulate = true)]
        private List<GameObject> pickUps;

        private Dictionary<string, PickupGenerationConfig> pickupsConfigsByName;

        private KeyPool<MonoBehaviour> pickupsPool;
        private Dictionary<string, PoolObject<MonoBehaviour>> pickupsByName;

        private void Awake()
        {
            pickupsByName = pickUps.ToDictionary(
                x => x.GetComponent<IPickUp>().PickupName,
                x => new PoolObject<MonoBehaviour>()
                {
                    PreloadCount = 0,
                    Template = x.GetComponent<MonoBehaviour>()
                }
            );
            pickupsPool = new KeyPool<MonoBehaviour>(pickupsByName, transform);
        }

        private void Start()
        {
            pickupsConfigsByName = enemiesPickups.EnemiesPickupsChances.ToDictionary(
                x => x.Key.Name,
                x => x.Value
            );
        }

        private bool PickupsFilter(GameObject obj)
        {
            return obj.TryGetComponent<IPickUp>(out _);
        }

        public void GeneratePickup(IEnemy enemy, Transform position)
        {
            if (
                pickupsConfigsByName.TryGetValue(enemy.EnemyName, out PickupGenerationConfig config)
            )
            {
                foreach (KeyValuePair<SelectablePickupName, Percentage> item in config.Chances)
                {
                    if (item.Value.TryChance())
                    {
                        Vector2 shift =
                            new(
                                Random.Range(-randomRange.x, randomRange.x),
                                Random.Range(-randomRange.y, randomRange.y)
                            );
                        MonoBehaviour pickup = pickupsPool.Get(item.Key.Name);
                        pickup.transform.position = position.position + (Vector3)shift;
                    }
                }
            }
        }
    }
}
