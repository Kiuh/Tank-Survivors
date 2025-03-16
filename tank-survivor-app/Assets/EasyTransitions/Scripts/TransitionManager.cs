using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace EasyTransition
{
    public class TransitionManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject transitionTemplate;

        private bool runningTransition;

        public UnityAction onTransitionBegin;
        public UnityAction onTransitionCutPointReached;
        public UnityAction onTransitionEnd;

        private static TransitionManager instance;

        private void Awake()
        {
            instance = this;
        }

        public static TransitionManager Instance()
        {
            if (instance == null)
            {
                Debug.LogError("You tried to access the instance before it exists.");
            }

            return instance;
        }

        /// <summary>
        /// Starts a transition without loading a new level.
        /// </summary>
        /// <param name="transition">The settings of the transition you want to use.</param>
        /// <param name="startDelay">The delay before the transition starts.</param>
        public void Transition(TransitionSettings transition, float startDelay)
        {
            if (transition == null || runningTransition)
            {
                Debug.LogError("You have to assing a transition.");
                return;
            }

            runningTransition = true;
            _ = StartCoroutine(Timer(startDelay, transition));
        }

        /// <summary>
        /// Loads the new Scene with a transition.
        /// </summary>
        /// <param name="sceneName">The name of the scene you want to load.</param>
        /// <param name="transition">The settings of the transition you want to use to load you new scene.</param>
        /// <param name="startDelay">The delay before the transition starts.</param>
        public void Transition(string sceneName, TransitionSettings transition, float startDelay)
        {
            if (transition == null || runningTransition)
            {
                Debug.LogError("You have to assing a transition.");
                return;
            }

            runningTransition = true;
            _ = StartCoroutine(Timer(sceneName, startDelay, transition));
        }

        /// <summary>
        /// Loads the new Scene with a transition.
        /// </summary>
        /// <param name="sceneIndex">The index of the scene you want to load.</param>
        /// <param name="transition">The settings of the transition you want to use to load you new scene.</param>
        /// <param name="startDelay">The delay before the transition starts.</param>
        public void Transition(int sceneIndex, TransitionSettings transition, float startDelay)
        {
            if (transition == null || runningTransition)
            {
                Debug.LogError("You have to assing a transition.");
                return;
            }

            runningTransition = true;
            _ = StartCoroutine(Timer(sceneIndex, startDelay, transition));
        }

        /// <summary>
        /// Gets the index of a scene from its name.
        /// </summary>
        /// <param name="sceneName">The name of the scene you want to get the index of.</param>
        private int GetSceneIndex(string sceneName)
        {
            return SceneManager.GetSceneByName(sceneName).buildIndex;
        }

        private IEnumerator Timer(
            string sceneName,
            float startDelay,
            TransitionSettings transitionSettings
        )
        {
            yield return new WaitForSecondsRealtime(startDelay);

            onTransitionBegin?.Invoke();

            GameObject template = Instantiate(transitionTemplate);
            template.GetComponent<Transition>().transitionSettings = transitionSettings;

            float transitionTime = transitionSettings.transitionTime;
            if (transitionSettings.autoAdjustTransitionTime)
            {
                transitionTime /= transitionSettings.transitionSpeed;
            }

            yield return new WaitForSecondsRealtime(transitionTime);

            onTransitionCutPointReached?.Invoke();

            SceneManager.LoadScene(sceneName);

            yield return new WaitForSecondsRealtime(transitionSettings.destroyTime);

            onTransitionEnd?.Invoke();
        }

        private IEnumerator Timer(
            int sceneIndex,
            float startDelay,
            TransitionSettings transitionSettings
        )
        {
            yield return new WaitForSecondsRealtime(startDelay);

            onTransitionBegin?.Invoke();

            GameObject template = Instantiate(transitionTemplate);
            template.GetComponent<Transition>().transitionSettings = transitionSettings;

            float transitionTime = transitionSettings.transitionTime;
            if (transitionSettings.autoAdjustTransitionTime)
            {
                transitionTime /= transitionSettings.transitionSpeed;
            }

            yield return new WaitForSecondsRealtime(transitionTime);

            onTransitionCutPointReached?.Invoke();

            SceneManager.LoadScene(sceneIndex);

            yield return new WaitForSecondsRealtime(transitionSettings.destroyTime);

            onTransitionEnd?.Invoke();
        }

        private IEnumerator Timer(float delay, TransitionSettings transitionSettings)
        {
            yield return new WaitForSecondsRealtime(delay);

            onTransitionBegin?.Invoke();

            GameObject template = Instantiate(transitionTemplate);
            template.GetComponent<Transition>().transitionSettings = transitionSettings;

            float transitionTime = transitionSettings.transitionTime;
            if (transitionSettings.autoAdjustTransitionTime)
            {
                transitionTime /= transitionSettings.transitionSpeed;
            }

            yield return new WaitForSecondsRealtime(transitionTime);

            onTransitionCutPointReached?.Invoke();

            template
                .GetComponent<Transition>()
                .OnSceneLoad(SceneManager.GetActiveScene(), LoadSceneMode.Single);

            yield return new WaitForSecondsRealtime(transitionSettings.destroyTime);

            onTransitionEnd?.Invoke();

            runningTransition = false;
        }

        private IEnumerator Start()
        {
            while (gameObject.activeInHierarchy)
            {
                //Check for multiple instances of the Transition Manager component
                int managerCount = GameObject
                    .FindObjectsByType<TransitionManager>(
                        FindObjectsInactive.Include,
                        FindObjectsSortMode.None
                    )
                    .Length;
                if (managerCount > 1)
                {
                    Debug.LogError(
                        $"There are {managerCount} Transition Managers in your scene. Please ensure there is only one Transition Manager in your scene or overlapping transitions may occur."
                    );
                }

                yield return new WaitForSecondsRealtime(1f);
            }
        }
    }
}
