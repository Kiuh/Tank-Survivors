using System;
using UnityEngine;

namespace General
{
    [AddComponentMenu("General.Timer")]
    public class Timer : MonoBehaviour
    {
        private float timer = 0;
        private bool isPaused;

        public float CurrentTime => timer;
        public bool IsPaused => isPaused;

        private TimeSpan TimeSpanTime => TimeSpan.FromSeconds(timer);
        public string FormattedCurrentTime => $"{TimeSpanTime.Minutes}м {TimeSpanTime.Seconds}c";

        private void Awake()
        {
            timer = 0;
        }

        private void Start()
        {
            StartTimer();
        }

        public void StartTimer()
        {
            isPaused = false;
        }

        public void StopTimer()
        {
            isPaused = true;
        }

        private void Update()
        {
            if (!isPaused)
            {
                timer += Time.deltaTime;
            }
        }
    }
}
