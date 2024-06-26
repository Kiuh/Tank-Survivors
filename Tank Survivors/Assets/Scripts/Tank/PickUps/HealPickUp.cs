using UnityEngine;

namespace Tank.PickUps
{
    [AddComponentMenu("Tank.PickUps.HealPickUp")]
    public class HealPickUp : MonoBehaviour, IPickUp
    {
        [SerializeField]
        private float healAmount;

        private bool grabbed;

        public bool Grabbed => grabbed;

        [SerializeField]
        private string pickupName;
        public string PickupName => pickupName;

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
