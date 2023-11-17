using UnityEngine;

namespace Tank.PickUps
{
    public class ArmorPickup : MonoBehaviour, IPickUp
    {
        [SerializeField]
        private float armorAmount;

        private bool grabbed;
        public bool Grabbed => grabbed;

        private void OnEnable()
        {
            grabbed = false;
        }

        public void Grab(TankImpl tank)
        {
            grabbed = true;
        }
    }
}
