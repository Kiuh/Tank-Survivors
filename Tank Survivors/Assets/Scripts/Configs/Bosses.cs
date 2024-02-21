using System.Collections.Generic;
using Enemies.Bosses;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "BossesConfig", menuName = "Configs/Enemies/BossesConfig")]
    public class Bosses : SerializedScriptableObject
    {
        [OdinSerialize]
        [ListDrawerSettings(DraggableItems = false)]
        [LabelText("All bosses")]
        public List<Boss> BossesList { get; private set; } = new();
    }
}
