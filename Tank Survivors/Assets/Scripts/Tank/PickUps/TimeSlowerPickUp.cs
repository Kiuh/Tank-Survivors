using System.Collections;
using UnityEngine;

namespace Tank.PickUps
{
    public class TimeSlowerPickUp : MonoBehaviour, IPickUp
    {
        [SerializeField]
        private float time;

        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float timeSlowerPercentage;

        private readonly float defaultTimeScale = 1.0f;

        private bool grabbed;
        public bool Grabbed => grabbed;

        private void OnEnable()
        {
            grabbed = false;
        }

        public void Grab(TankImpl tank)
        {
            grabbed = true;
            _ = StartCoroutine(StartTimeSLower());
        }

        private IEnumerator StartTimeSLower()
        {
            Time.timeScale = timeSlowerPercentage;
            yield return new WaitForSecondsRealtime(time);
            Time.timeScale = defaultTimeScale;
        }
    }
}
