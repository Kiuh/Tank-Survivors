using System;
using System.Collections.Generic;
using System.Linq;
using DataStructs;
using Enemies;
using Sirenix.OdinInspector;
using Tank.PickUps;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Configs
{
    [Serializable]
    public struct SelectableEnemyName
    {
        [SerializeField]
        [ValueDropdown("@GetNamesList()")]
        private string name;
        public string Name => name;

#if UNITY_EDITOR
        private IEnumerable<string> GetNamesList()
        {
            string[] guids = AssetDatabase.FindAssets(
                "t:Prefab",
                new string[] { "Assets/Prefabs/Enemies" }
            );

            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                GameObject gameObject = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                if (gameObject.TryGetComponent(out IEnemy enemy))
                {
                    yield return enemy.EnemyName;
                }
            }
        }
#endif
    }

    [Serializable]
    public class SelectablePickupName
    {
        [SerializeField]
        [ValueDropdown("@GetNamesList()")]
        private string name;
        public string Name => name;

#if UNITY_EDITOR
        private IEnumerable<string> GetNamesList()
        {
            string[] guids = AssetDatabase.FindAssets(
                "t:Prefab",
                new string[] { "Assets/Prefabs/PickUps" }
            );

            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                GameObject gameObject = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                if (gameObject.TryGetComponent(out IPickUp enemy))
                {
                    yield return enemy.PickupName;
                }
            }
        }
#endif
    }

    [Serializable]
    public class PickupGenerationConfig
    {
        [Serializable]
        private struct PickupNamePercentage
        {
            public SelectablePickupName SelectablePickupName;
            public Percentage Percentage;
        }

        [SerializeField]
        private List<PickupNamePercentage> pickupNamePercentages;
        private Dictionary<SelectablePickupName, Percentage> chances = new();

        public Dictionary<SelectablePickupName, Percentage> Chances
        {
            get
            {
                chances ??= pickupNamePercentages.ToDictionary(
                    x => x.SelectablePickupName,
                    x => x.Percentage
                );
                return chances;
            }
        }
    }

    [CreateAssetMenu(
        fileName = "EnemiesPickupsDropsConfig",
        menuName = "Configs/EnemiesPickupsDropsConfig",
        order = 1
    )]
    public class EnemiesPickupsDrops : ScriptableObject
    {
        [Serializable]
        private struct EnemyNamePickupGeneration
        {
            public SelectableEnemyName SelectableEnemyName;
            public PickupGenerationConfig PickupGenerationConfig;
        }

        [SerializeField]
        private List<EnemyNamePickupGeneration> enemyNamePercentages;

        private Dictionary<SelectableEnemyName, PickupGenerationConfig> chances = new();

        public Dictionary<SelectableEnemyName, PickupGenerationConfig> EnemiesPickupsChances
        {
            get
            {
                chances ??= enemyNamePercentages.ToDictionary(
                    x => x.SelectableEnemyName,
                    x => x.PickupGenerationConfig
                );
                return chances;
            }
        }
    }
}
