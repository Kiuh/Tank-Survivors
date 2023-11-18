using UnityEngine;

namespace Tank.PickUps
{
    [AddComponentMenu("Tank.PickUps.ArmorPickup")]
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
            tank.FixArmor(armorAmount);
        }
    }
}
