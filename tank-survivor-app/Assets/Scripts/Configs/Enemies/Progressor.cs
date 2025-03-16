using System;
using System.Collections.Generic;
using Enemies;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "ProgressorConfig", menuName = "Configs/ProgressorConfig")]
    public class Progressor : ScriptableObject
    {
        [Serializable]
        public enum Mode
        {
            Source,
            Current
        }

        [SerializeField]
        [Unit(Units.Second)]
        private float interval;

        public float Interval
        {
            get => interval;
            private set => interval = value;
        }

        [SerializeField]
        [EnumToggleButtons]
        private Mode currentMode = Mode.Source;

        public Mode CurrentMode
        {
            get => currentMode;
            private set => currentMode = value;
        }

        [SerializeReference]
        private List<IModuleUpgrade> upgradableModules = new();
        public List<IModuleUpgrade> UpgradableModules
        {
            get => upgradableModules;
            set => upgradableModules = value;
        }
    }
}
