using Sirenix.OdinInspector;
using UnityEngine;

namespace Tank.PickUps
{
    [AddComponentMenu("Tank.PickUps.ExperiencePickUp")]
    public class ExperiencePickUp : MonoBehaviour, IPickUp
    {
        [SerializeField]
        private float experienceAmount;

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
            tank.PlayerLevel.AddExperience(experienceAmount);
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
