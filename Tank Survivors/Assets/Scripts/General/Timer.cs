using UnityEngine;

namespace General
{
    [AddComponentMenu("General.Timer")]
    public class Timer : MonoBehaviour
    {
        private float timer = 0;
        public float CurrentTime => timer;

        private void Awake()
        {
            timer = 0;
        }

        private void Update()
        {
            timer += Time.deltaTime;
        }
    }
}
