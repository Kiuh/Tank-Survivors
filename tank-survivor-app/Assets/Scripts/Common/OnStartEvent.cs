using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Common
{
    [AddComponentMenu("Scripts/Common/Common.OnStartEvent")]
    internal class OnStartEvent : MonoBehaviour
    {
        public UnityEvent OnStart;

        [SerializeField]
        private float delay;

        private IEnumerator Start()
        {
            yield return new WaitForSecondsRealtime(delay);
            OnStart?.Invoke();
        }
    }
}
