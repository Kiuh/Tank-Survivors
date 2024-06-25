using System.Collections;
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
        private SpriteRenderer spriteRenderer;

        private readonly float defaultTimeScale = 1.0f;

        private bool grabbed;
        public bool Grabbed => grabbed;

        [SerializeField]
        private string pickupName;
        public string PickupName
        {
            get => pickupName;
            private set => pickupName = value;
        }

        private void OnEnable()
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
            yield return new WaitForSecondsRealtime(time);
            Time.timeScale = defaultTimeScale;
            Destroy(gameObject);
        }
    }
}
