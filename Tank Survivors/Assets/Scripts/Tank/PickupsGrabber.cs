using Tank.PickUps;
using UnityEngine;

namespace Tank
{
    [RequireComponent(typeof(CircleCollider2D))]
    [AddComponentMenu("Tank.PickupsGrabber")]
    public class PickupsGrabber : MonoBehaviour
    {
        [SerializeField]
        private TankImpl tank;

        [SerializeField]
        private CircleCollider2D circleCollider;

        private void Update()
        {
            circleCollider.radius = tank.PickupRadius.GetModifiedValue();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IPickUp pickUp) && !pickUp.Grabbed)
            {
                pickUp.Grab(tank);
            }
        }
    }
}
