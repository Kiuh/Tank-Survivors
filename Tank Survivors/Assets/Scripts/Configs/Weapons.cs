using System.Collections.Generic;
using System.Linq;
using Tank.Weapons;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "WeaponsConfig", menuName = "Configs/WeaponsConfig", order = 3)]
    public class Weapons : ScriptableObject
    {
        [SerializeField]
        private List<SerializedWeapon> weapons;
        public IEnumerable<IWeapon> GetWeapons => weapons.Select(x => x.ToWeapon());
    }
}
