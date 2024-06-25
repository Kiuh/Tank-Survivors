using System.Collections;
using UnityEngine;

namespace Common
{
    [AddComponentMenu("Scripts/Common/Common.TimeScaleSetter")]
    internal class TimeScaleSetter : MonoBehaviour
    {
        [SerializeField]
        private float newTimeScale = 1.0f;

        [SerializeField]
        private float delay = 1.0f;

        private IEnumerator Start()
        {
            yield return new WaitForSecondsRealtime(delay);
            Time.timeScale = newTimeScale;
        }
    }
}
