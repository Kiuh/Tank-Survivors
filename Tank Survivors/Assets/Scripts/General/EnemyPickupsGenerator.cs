using System.Collections.Generic;
using System.Linq;
using Configs;
using DataStructs;
using Enemies;
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
        private Dictionary<string, GameObject> pickupsByName;
        private Dictionary<string, PickupGenerationConfig> pickupsConfigsByName;

        private void Start()
        {
            pickupsByName = pickUps.ToDictionary(
                x => x.GetComponent<IPickUp>().PickupName,
                x => x.gameObject
            );
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
                        _ = Instantiate(
                            pickupsByName[item.Key.Name],
                            position.position + (Vector3)shift,
                            Quaternion.identity,
                            pickupsParent
                        );
                    }
                }
            }
        }
    }
}
