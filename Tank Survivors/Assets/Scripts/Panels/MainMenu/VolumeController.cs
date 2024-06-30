using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using YG;

namespace Panels.MainMenu
{
    [AddComponentMenu("Scripts/Panels/MainMenu/Panels.MainMenu.VolumeController")]
    internal class VolumeController : MonoBehaviour
    {
        [SerializeField]
        private Toggle musicToggle;

        [SerializeField]
        private Toggle soundsToggle;

        private void Start()
        {
            musicToggle.onValueChanged.AddListener(SwitchMusic);
            soundsToggle.onValueChanged.AddListener(SwitchSounds);

            musicToggle.isOn = YandexGame.savesData.IsMusicOn;
            soundsToggle.isOn = YandexGame.savesData.IsSoundsOn;
        }

        [SerializeField]
        private AudioMixer audioMixer;

        public const string MUSIC_VOLUME_LABEL = "Music";
        public const string SOUND_VOLUME_LABEL = "Sounds";

        public void SwitchMusic(bool value)
        {
            SetMusicVolume(value ? 1 : 0);
            YandexGame.savesData.IsMusicOn = value;
            YandexGame.SaveProgress();
        }

        public void SwitchSounds(bool value)
        {
            SetSoundsVolume(value ? 1 : 0);
            YandexGame.savesData.IsSoundsOn = value;
            YandexGame.SaveProgress();
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
