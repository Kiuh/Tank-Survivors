using System.Collections;
using UnityEngine;

namespace General
{
    [AddComponentMenu("General.Timer")]
    public class Timer : MonoBehaviour
    {
        private float timer = 0;
        private bool isPaused;
        private Coroutine coroutine;
        public float CurrentTime => timer;
        public bool IsPaused => isPaused;

        private System.TimeSpan TimeSpanTime => System.TimeSpan.FromSeconds(timer);
        public string FormattedCurrentTime => $"{TimeSpanTime.Minutes}м {TimeSpanTime.Seconds}c";

        private void Awake()
        {
            timer = 0;
            StartTimer();
        }

        public void StartTimer()
        {
            isPaused = false;
            coroutine = StartCoroutine(CountTime());
        }

        public void StopTimer()
        {
            isPaused = true;
            StopCoroutine(coroutine);
        }

        private IEnumerator CountTime()
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();
                timer += Time.deltaTime;
            }
        }
    }
}
