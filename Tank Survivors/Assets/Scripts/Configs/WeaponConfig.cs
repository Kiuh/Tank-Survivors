using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Tank.Weapons;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "Configs/WeaponConfig")]
    public class WeaponConfig : SerializedScriptableObject
    {
        [OdinSerialize]
        [HideLabel]
        [InlineProperty]
        public IWeapon Weapon { get; private set; }
    }
}
