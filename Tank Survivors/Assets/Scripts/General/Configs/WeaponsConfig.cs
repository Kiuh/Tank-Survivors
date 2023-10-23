using System.Collections.Generic;
using System.Linq;
using Tank.Weapons;
using UnityEngine;

namespace General.Configs
{
    [CreateAssetMenu(fileName = "WeaponsConfig", menuName = "Configs/WeaponsConfig", order = 3)]
    public class WeaponsConfig : ScriptableObject
    {
        [SerializeField]
        private List<SerializedWeapon> weapons;
        public IEnumerable<IWeapon> Weapons => weapons.Select(x => x.ToWeapon());
    }
}
