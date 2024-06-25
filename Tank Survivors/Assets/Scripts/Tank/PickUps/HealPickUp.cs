using Sirenix.OdinInspector;
using UnityEngine;

namespace Tank.PickUps
{
    [AddComponentMenu("Tank.PickUps.HealPickUp")]
    public class HealPickUp : SerializedMonoBehaviour, IPickUp
    {
        [SerializeField]
        private float healAmount;

        private bool grabbed;

        public bool Grabbed => grabbed;

        [SerializeField]
        private string pickupName;
        public string PickupName
        {
            get => pickupName;
            private set => pickupName = value;
        }

        private void OnEnable()
        {
            grabbed = false;
        }

        public void Grab(TankImpl tank)
        {
            grabbed = true;
            tank.Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
