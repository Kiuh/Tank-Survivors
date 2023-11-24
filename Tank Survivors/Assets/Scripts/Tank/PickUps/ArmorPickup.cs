using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Tank.PickUps
{
    [AddComponentMenu("Tank.PickUps.ArmorPickup")]
    public class ArmorPickup : SerializedMonoBehaviour, IPickUp
    {
        [SerializeField]
        private float armorAmount;

        public bool Grabbed { get; private set; }

        [OdinSerialize]
        public string PickupName { get; private set; }

        private void OnEnable()
        {
            Grabbed = false;
        }

        public void Grab(TankImpl tank)
        {
            Grabbed = true;
            tank.FixArmor(armorAmount);
            Destroy(gameObject);
        }
    }
}
