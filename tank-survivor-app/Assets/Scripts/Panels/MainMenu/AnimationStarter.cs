using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Panels.MainMenu
{
    [AddComponentMenu("Scripts/Panels/MainMenu/Panels.MainMenu.AnimationStarter")]
    internal class AnimationStarter : MonoBehaviour
    {
        private static bool is_first_open = true;

        [SerializeField]
        private GameObject toDestroy;

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private float delay;

        public UnityEvent OnEnterAnimationStarted;

        private void Awake()
        {
            SceneManager.sceneLoaded += StartAnimation;
        }

        private void StartAnimation(Scene scene, LoadSceneMode sceneMode)
        {
            if (is_first_open)
            {
                is_first_open = false;
                _ = StartCoroutine(WaitAndDestroy());
                return;
            }
            Destroy(toDestroy);
        }

        private IEnumerator WaitAndDestroy()
        {
            OnEnterAnimationStarted?.Invoke();
            animator.enabled = true;
            animator.speed = 1.5f;
            yield return new WaitForSecondsRealtime(delay);
            Destroy(toDestroy);
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= StartAnimation;
        }
    }
}
