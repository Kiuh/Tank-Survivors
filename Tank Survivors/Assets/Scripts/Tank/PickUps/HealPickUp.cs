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

        [ReadOnly]
        [SerializeField]
        private TankImpl tank;

        [SerializeField]
        private float followSpeed;

        public void Grab(TankImpl tank)
        {
            grabbed = true;
            this.tank = tank;
        }

        private void Update()
        {
            if (grabbed)
            {
                transform.position = Vector3.Lerp(
                    transform.position,
                    tank.transform.position,
                    Time.deltaTime * followSpeed
                );
            }
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out TankImpl tank))
            {
                CompleteGrab(tank);
            }
        }

        private void CompleteGrab(TankImpl tank)
        {
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
