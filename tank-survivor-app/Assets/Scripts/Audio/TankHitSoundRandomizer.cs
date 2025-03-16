using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Audio
{
    [AddComponentMenu("Scripts/Audio/Audio.TankHitSoundRandomizer")]
    internal class TankHitSoundRandomizer : MonoBehaviour
    {
        [SerializeField]
        private List<InvokeSoundPlayer> invokeSoundPlayers;

        [Button]
        public void PlayRandomSound()
        {
            invokeSoundPlayers[UnityEngine.Random.Range(0, invokeSoundPlayers.Count)].PlaySound();
        }
    }
}
