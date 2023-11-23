using UnityEngine;

namespace Tank.PickUps
{
    [AddComponentMenu("Tank.PickUps.ExperiencePickUp")]
    public class ExperiencePickUp : MonoBehaviour, IPickUp
    {
        [SerializeField]
        private float experienceAmount;

        private bool grabbed;
        public bool Grabbed => grabbed;

        public void Initialize(float experienceAmount)
        {
            this.experienceAmount = experienceAmount;
        }

        public void Grab(TankImpl tank)
        {
            grabbed = true;
            tank.PlayerLevel.AddExperience(experienceAmount);
            Destroy(gameObject);
        }
    }
}
