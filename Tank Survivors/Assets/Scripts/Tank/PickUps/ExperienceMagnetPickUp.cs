using UnityEngine;

namespace Tank.PickUps
{
    public class ExperienceMagnetPickUp : MonoBehaviour, IPickUp
    {
        [SerializeField]
        private CircleCollider2D magnetZone;

        [SerializeField]
        private float time;

        private bool grabbed;
        public bool Grabbed => grabbed;

        private void OnEnable()
        {
            grabbed = true;
        }

        public void Grab(TankImpl tank) { }
    }
}
