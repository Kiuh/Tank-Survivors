using UnityEngine;

namespace Audio
{
    [AddComponentMenu("Scripts/Audio/Audio.BackgroundMusic")]
    internal class BackgroundMusic : MonoBehaviour
    {
        public static BackgroundMusic Instance;

        [SerializeField]
        private AudioSource audioSource;

        private void Awake()
        {
            if (Instance == null)
            {
                DontDestroyOnLoad(this);
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void RestartMusic()
        {
            audioSource.Play();
        }
    }
}
