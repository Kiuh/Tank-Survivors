using Common;
using System;
using UnityEngine;

namespace DataStructs
{
    [Serializable]
    public class Experience
    {
        [SerializeField]
        private ModifiableValueContainer valueContainer;
        public ModifiableValueContainer ValueContainer => valueContainer;

        [SerializeField]
        [InspectorReadOnly]
        private uint currentLevel = 0;
        public uint CurrentLevel => currentLevel;

        public event Action OnLevelUp;

        public void AddExperience(float value)
        {
            // TODO: implement
            OnLevelUp?.Invoke();
        }
    }
}
