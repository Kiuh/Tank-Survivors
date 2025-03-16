using Assets.Scripts.Audio;
using UnityEngine;

namespace Audio
{
    [AddComponentMenu("Scripts/Audio/Audio.BackgroundSoundPlayer")]
    internal class BackgroundSoundPlayer : MonoBehaviour, IPauseSound
    {
        [SerializeField]
        private AudioSource source;

        private void Start()
        {
            SoundsManager.Instance.Register(this);
        }

        public void Pause()
        {
            source.Pause();
        }

        public void UnPause()
        {
            source.UnPause();
        }

        private void OnDestroy()
        {
            SoundsManager.Instance.Unregister(this);
        }
    }
}
