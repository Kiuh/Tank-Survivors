using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;

namespace Panels.MainMenu
{
    [AddComponentMenu("Scripts/Panels/MainMenu/Panels.MainMenu.MusicSettingsSetter")]
    internal class MusicSettingsSetter : MonoBehaviour
    {
        [Button]
        private void Awake()
        {
            _ = StartCoroutine(ResetAudioSettings());
        }

        private IEnumerator ResetAudioSettings()
        {
            yield return new WaitUntil(() => SaveSystem.IsSaveSystemAvailable);
            SwitchMusic(SaveSystem.GetMusicValue());
            SwitchSounds(SaveSystem.GetSoundsValue());
        }

        [SerializeField]
        private AudioMixer audioMixer;

        public const string MUSIC_VOLUME_LABEL = "Music";
        public const string SOUND_VOLUME_LABEL = "Sounds";

        public void SwitchMusic(bool value)
        {
            SetMusicVolume(value ? 1 : 0);
            SaveSystem.SetMusicValue(value);
        }

        public void SwitchSounds(bool value)
        {
            SetSoundsVolume(value ? 1 : 0);
            SaveSystem.SetSoundsValue(value);
        }

        private void SetMusicVolume(float value)
        {
            _ = audioMixer.SetFloat(MUSIC_VOLUME_LABEL, CalculateVolume(value));
        }

        private void SetSoundsVolume(float value)
        {
            _ = audioMixer.SetFloat(SOUND_VOLUME_LABEL, CalculateVolume(value));
        }

        public const float MIN_DB = -80;
        public const float MAX_DB = 20;

        private float CalculateVolume(float value)
        {
            return Mathf.Clamp(20 * Mathf.Log10(value), MIN_DB, MAX_DB);
        }
    }
}
