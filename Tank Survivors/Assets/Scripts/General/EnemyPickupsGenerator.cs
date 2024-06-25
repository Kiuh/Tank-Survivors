using System.Collections.Generic;
using System.Linq;
using Configs;
using DataStructs;
using Enemies;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Tank.PickUps;
using UnityEngine;

namespace General
{
    public class EnemyPickupsGenerator : SerializedMonoBehaviour
    {
        [SerializeField]
        private EnemiesPickupsDrops enemiesPickups;

        [SerializeField]
        private Transform pickupsParent;

        [OdinSerialize]
        [AssetList(CustomFilterMethod = "PickupsFilter", AutoPopulate = true)]
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
                        _ = Instantiate(
                            pickupsByName[item.Key.Name],
                            position.position,
                            Quaternion.identity,
                            pickupsParent
                        );
                    }
                }
            }
        }
    }
}
