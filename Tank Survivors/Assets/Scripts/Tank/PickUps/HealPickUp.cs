using Sirenix.OdinInspector;
using UnityEngine;

namespace Tank.PickUps
{
    [AddComponentMenu("Tank.PickUps.HealPickUp")]
    public class HealPickUp : MonoBehaviour, IPickUp
    {
        [SerializeField]
        private float healAmount;

        [SerializeField]
        private string effectText;

        [SerializeField]
        private Color effectColor;

        [SerializeField]
        private bool grabbed = false;
        public bool Grabbed => grabbed;

        [SerializeField]
        private string pickupName;
        public string PickupName => pickupName;

        [Required]
        [SerializeField]
        private FloatingEffect floatingEffect;

        public void Grab(TankImpl tank)
        {
            grabbed = true;
            tank.Heal(healAmount);
            FloatingEffect effect = Instantiate(
                floatingEffect,
                transform.position,
                Quaternion.identity
            );
            effect.Launch(effectText, effectColor);
            Destroy(gameObject);
        }
    }
}
