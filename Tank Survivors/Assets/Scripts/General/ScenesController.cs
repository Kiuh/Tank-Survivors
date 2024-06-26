﻿using System;
using System.Collections.Generic;
using Common;
using EasyTransition;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace General
{
    public enum InGameScene
    {
        MainScene,
        LevelsScene,
        GameplayScene
    }

    [Serializable]
    public struct SceneWithName
    {
        public InGameScene InGameScene;

        [Scene]
        public string SceneName;
    }

    [AddComponentMenu("General.ScenesController")]
    internal class ScenesController : MonoBehaviour
    {
        [SerializeField]
        private List<SceneWithName> scenesWithName;

        public static ScenesController Instance;

        [Required]
        [SerializeField]
        private TransitionSettings transitionSettings;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        public void LoadScene(InGameScene inGameScene)
        {
            string name = scenesWithName.Find(x => x.InGameScene == inGameScene).SceneName;
            TransitionManager.Instance().Transition(name, transitionSettings, 0);
        }
    }
}
