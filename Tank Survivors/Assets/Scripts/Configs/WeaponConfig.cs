using Sirenix.OdinInspector;
using Tank.Weapons;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "Configs/WeaponConfig")]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeReference]
        [HideLabel]
        [InlineProperty]
        private IWeapon weapon;
        public IWeapon Weapon
        {
            get => weapon;
            private set => weapon = value;
        }
    }
}
