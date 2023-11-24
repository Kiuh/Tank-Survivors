﻿using DataStructs;
using Enemies;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using Tank.PickUps;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Configs
{
    [Serializable]
    [HideReferenceObjectPicker]
    public class SelectableEnemyName
    {
        [OdinSerialize]
        [ValueDropdown("@GetNamesList()")]
        [ShowInInspector]
        public string Name { get; private set; }

        private IEnumerable<string> GetNamesList()
        {
#if UNITY_EDITOR
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
#endif
        }
    }

    [Serializable]
    [HideReferenceObjectPicker]
    public class SelectablePickupName
    {
        [OdinSerialize]
        [ValueDropdown("@GetNamesList()")]
        [ShowInInspector]
        public string Name { get; private set; }

        private IEnumerable<string> GetNamesList()
        {
#if UNITY_EDITOR
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
#endif
        }
    }

    [Serializable]
    [HideReferenceObjectPicker]
    public class PickupGenerationConfig
    {
        [OdinSerialize]
        public Dictionary<SelectablePickupName, Percentage> Chances { get; private set; } = new();
    }

    [CreateAssetMenu(
        fileName = "EnemiesPickupsDropsConfig",
        menuName = "Configs/EnemiesPickupsDropsConfig",
        order = 1
    )]
    public class EnemiesPickupsDrops : SerializedScriptableObject
    {
        [OdinSerialize]
        public Dictionary<SelectableEnemyName, PickupGenerationConfig> EnemiesPickupsChances
        {
            get;
            private set;
        } = new();
    }
}
