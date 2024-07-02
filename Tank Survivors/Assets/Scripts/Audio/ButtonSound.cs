using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Audio
{
    [RequireComponent(typeof(Button))]
    [AddComponentMenu("Scripts/Audio/Audio.ButtonSound")]
    internal class ButtonSound : MonoBehaviour
    {
        [AssetList]
        [SerializeField]
        private InvokeSoundPlayer invokeSoundPlayerPrefab;

        [Required]
        [SerializeField]
        private AudioClip audioClip;

        [SerializeField]
        private float volume = 1.0f;

        private InvokeSoundPlayer invokeSoundPlayer;
        private Button button;

        private void Start()
        {
            invokeSoundPlayer = Instantiate(invokeSoundPlayerPrefab, transform);
            invokeSoundPlayer.AudioClip = audioClip;
            invokeSoundPlayer.Volume = volume;
            invokeSoundPlayer.IsStoppable = false;
            button = GetComponent<Button>();
            button.onClick.AddListener(() => invokeSoundPlayer.PlaySound());
        }
    }
}
