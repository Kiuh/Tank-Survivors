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

        [OdinSerialize]
        [AssetList(CustomFilterMethod = "PickupsFilter", AutoPopulate = true)]
        private List<GameObject> pickUps;

        private bool PickupsFilter(GameObject obj)
        {
            return obj.TryGetComponent<IPickUp>(out _);
        }

        public void GeneratePickup(IEnemy enemy, Transform position)
        {
            if (
                enemiesPickups
                    .EnemiesPickupsChances.Keys.Where(x => x.Name == enemy.EnemyName)
                    .Count() > 0
            )
            {
                KeyValuePair<SelectableEnemyName, PickupGenerationConfig> pair =
                    enemiesPickups.EnemiesPickupsChances.First(x => x.Key.Name == enemy.EnemyName);
                foreach (KeyValuePair<SelectablePickupName, Percentage> item in pair.Value.Chances)
                {
                    if (item.Value.TryChance())
                    {
                        _ = Instantiate(
                            pickUps.First(x =>
                                x.GetComponent<IPickUp>().PickupName == item.Key.Name
                            ),
                            position.position,
                            Quaternion.identity
                        );
                    }
                }
            }
        }
    }
}
