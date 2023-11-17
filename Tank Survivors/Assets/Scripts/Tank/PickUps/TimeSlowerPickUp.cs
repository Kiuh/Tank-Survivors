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
        private float timeSlowerStrength;

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
            Time.timeScale = timeSlowerStrength;
            yield return new WaitForSeconds(time);
            Time.timeScale = 1.0f;
        }
    }
}
