using System.Collections.Generic;
using Assets.Scripts.Audio;
using UnityEngine;

namespace Audio
{
    [AddComponentMenu("Scripts/Audio/Audio.SoundsManager")]
    internal class SoundsManager : MonoBehaviour
    {
        public static SoundsManager Instance;

        private List<IPauseSound> soundPlayers = new();

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

        public void PauseSounds()
        {
            soundPlayers.ForEach(x => x.Pause());
        }

        public void UnPauseSounds()
        {
            soundPlayers.ForEach(x => x.UnPause());
        }

        public void Register(IPauseSound soundPlayer)
        {
            if (!soundPlayers.Contains(soundPlayer))
            {
                soundPlayers.Add(soundPlayer);
            }
        }

        public void Unregister(IPauseSound soundPlayer)
        {
            _ = soundPlayers.Remove(soundPlayer);
        }
    }
}
