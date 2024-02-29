using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Tank;
using UnityEngine;

namespace Enemies
{
    [AddComponentMenu("Enemies.Soldier")]
    public class Soldier : SerializedMonoBehaviour, IEnemy
    {
        public string EnemyName { get; private set; }

        [OdinSerialize]
        [ListDrawerSettings(
            HideAddButton = true,
            HideRemoveButton = true,
            AlwaysAddDefaultValue = true,
            DraggableItems = false
        )]
        [FoldoutGroup("Modules")]
        public List<IModule> Modules { get; set; }

        public event Action OnDeath;

        private TankImpl tank;

        public void Initialize(TankImpl tank)
        {
            this.tank = tank;
        }

        public void TakeDamage(float damageAmount) { }

        [Button]
        [FoldoutGroup("Modules")]
        private void RefreshModules()
        {
            Modules = new() { new MovementModule() };
        }
    }
}
