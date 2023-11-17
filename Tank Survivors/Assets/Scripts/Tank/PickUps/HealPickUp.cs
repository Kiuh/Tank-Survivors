using UnityEngine;

namespace Tank.PickUps
{
    public class HealPickUp : MonoBehaviour, IPickUp
    {
        [SerializeField]
        private float healAmount;

        private bool grabbed;
        public bool Grabbed => grabbed;

        private void OnEnable()
        {
            grabbed = false;
        }

        public void Grab(TankImpl tank)
        {
            grabbed = true;
            tank.Heal(healAmount);
        }
    }
}
