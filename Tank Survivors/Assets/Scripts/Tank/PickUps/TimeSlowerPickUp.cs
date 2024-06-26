using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tank.PickUps
{
    [AddComponentMenu("Tank.PickUps.TimeSlowerPickUp")]
    public class TimeSlowerPickUp : MonoBehaviour, IPickUp
    {
        [SerializeField]
        private float time;

        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float timeSlowerPercentage;

        [SerializeField]
        private string effectText;

        [SerializeField]
        private Color effectColor;

        [Required]
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private bool grabbed;
        public bool Grabbed => grabbed;

        [SerializeField]
        private string pickupName;
        public string PickupName => pickupName;

        [Required]
        [SerializeField]
        private FloatingEffect floatingEffect;

        private void Awake()
        {
            grabbed = false;
        }

        public void Grab(TankImpl tank)
        {
            grabbed = true;
            _ = StartCoroutine(StartTimeSLower());
            spriteRenderer.enabled = false;
        }

        private IEnumerator StartTimeSLower()
        {
            Time.timeScale = timeSlowerPercentage;
            FloatingEffect effect = Instantiate(
                floatingEffect,
                transform.position,
                Quaternion.identity
            );
            effect.Launch(effectText, effectColor);
            yield return new WaitForSecondsRealtime(time);
            Time.timeScale = 1.0f;
            Destroy(gameObject);
        }
    }
}
