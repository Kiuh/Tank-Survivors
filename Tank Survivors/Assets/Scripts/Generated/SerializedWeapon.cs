// <auto-generated/>

using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using Assets.Scripts.Tank.Weapons;

namespace Tank.Weapons 
{
    [Serializable]
    [HideReferenceObjectPicker]
    public class SerializedWeapon
    {
        private static string[] names = new string[] { "DoubleShotGun", "RailGun", "BasicGun", "GrenadeLauncherGun", "Minigun" };

        [ShowInInspector]
        [ValueDropdown("names")]
        private string selectedType;

        [ShowInInspector]
        [NonSerialized, OdinSerialize]
        [ShowIf("@this.selectedType == \"DoubleShotGun\"")]
        private DoubleShotGun doubleShotGun;

        [ShowInInspector]
        [NonSerialized, OdinSerialize]
        [ShowIf("@this.selectedType == \"RailGun\"")]
        private RailGun railGun;

        [ShowInInspector]
        [NonSerialized, OdinSerialize]
        [ShowIf("@this.selectedType == \"BasicGun\"")]
        private BasicGun basicGun;

        [ShowInInspector]
        [NonSerialized, OdinSerialize]
        [ShowIf("@this.selectedType == \"GrenadeLauncherGun\"")]
        private GrenadeLauncherGun grenadeLauncherGun;

        [ShowInInspector]
        [NonSerialized, OdinSerialize]
        [ShowIf("@this.selectedType == \"Minigun\"")]
        private Minigun minigun;

        public IWeapon ToWeapon()
        {
            return selectedType switch
            {
                "DoubleShotGun" => doubleShotGun,
                "RailGun" => railGun,
                "BasicGun" => basicGun,
                "GrenadeLauncherGun" => grenadeLauncherGun,
                "Minigun" => minigun,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
