using System.Collections.Generic;
using Assets.Scripts.Audio;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Audio
{
    [AddComponentMenu("Scripts/Audio/Audio.InvokeSoundPlayer")]
    internal class InvokeSoundPlayer : MonoBehaviour, IPauseSound
    {
        [AssetsOnly]
        [AssetList]
        [SerializeField]
        private AudioSource sourcePrefab;

        [RequiredIn(PrefabKind.PrefabInstanceAndNonPrefabInstance)]
        [SerializeField]
        private AudioClip audioClip;

        [SerializeField]
        private bool isStoppable = true;

        [SerializeField]
        private float volume = 1.0f;

        private List<AudioSource> sources = new();
        private List<Tween> destroyTweens = new();

        public AudioClip AudioClip
        {
            get => audioClip;
            set => audioClip = value;
        }
        public float Volume
        {
            get => volume;
            set => volume = value;
        }
        public bool IsStoppable
        {
            get => isStoppable;
            set => isStoppable = value;
        }

        private void Start()
        {
            SoundsManager.Instance.Register(this);
        }

        public void PlaySound()
        {
            AudioSource source = Instantiate(sourcePrefab, SoundsManager.Instance.transform);
            source.clip = AudioClip;
            source.volume = Volume;
            source.Play();
            sources.Add(source);
            Tween destroyTween = DOVirtual.DelayedCall(
                source.clip.length,
                () =>
                {
                    _ = sources.Remove(source);
                    Destroy(source.gameObject);
                },
                false
            );
            destroyTweens.Add(destroyTween);
        }

        public void Pause()
        {
            if (!IsStoppable)
            {
                return;
            }

            sources.ForEach(x => x.Pause());
            destroyTweens.ForEach(x => x.Pause());
        }

        public void UnPause()
        {
            if (!IsStoppable)
            {
                return;
            }

            destroyTweens.ForEach(x => x.Play());
            sources.ForEach(x => x.UnPause());
        }

        private void OnDestroy()
        {
            SoundsManager.Instance.Unregister(this);
        }
    }
}
