using UnityEngine;

namespace Audio
{
    [AddComponentMenu("Scripts/Audio/Audio.MusicRestarter")]
    internal class MusicRestarter : MonoBehaviour
    {
        [SerializeField]
        private bool restartMusicOnStart;

        private void Start()
        {
            if (restartMusicOnStart)
            {
                BackgroundMusic.Instance.RestartMusic();
            }
        }
    }
}
