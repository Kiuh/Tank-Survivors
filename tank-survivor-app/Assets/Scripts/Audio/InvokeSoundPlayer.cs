using System;
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

        [SerializeField]
        private bool enableDelay = false;

        [ShowIf(nameof(enableDelay))]
        [SerializeField]
        private float delay;

        [ShowIf(nameof(enableDelay))]
        [SerializeField]
        private int maxDelayCalls = 4;

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

        private int delayedCalls = 0;
        private float timer = 0;

        public void PlaySound()
        {
            if (enableDelay)
            {
                if (timer < 0)
                {
                    PlaySoundInternal();
                    timer = delay;
                }
                else
                {
                    delayedCalls++;
                    delayedCalls = Math.Min(maxDelayCalls, delayedCalls);
                }
            }
            else
            {
                PlaySoundInternal();
            }
        }

        private void Update()
        {
            if (timer >= 0)
            {
                timer -= Time.deltaTime;
                if (timer < 0 && delayedCalls > 0)
                {
                    timer = delay;
                    PlaySoundInternal();
                    delayedCalls--;
                }
            }
        }

        private void PlaySoundInternal()
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
            _ = destroyTween.OnComplete(() => destroyTweens.Remove(destroyTween));
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
