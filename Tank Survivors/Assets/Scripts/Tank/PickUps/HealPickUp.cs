using Sirenix.OdinInspector;
using Sirenix.Serialization;
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

        [OdinSerialize]
        public string PickupName { get; private set; }

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
