using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Tank
{
    [AddComponentMenu("Scripts/Tank/Tank.TrackGenerator")]
    internal class TrackGenerator : MonoBehaviour
    {
        [SerializeField]
        private Track track;

        [SerializeField]
        private List<Transform> dots;

        [SerializeField]
        private float interval;

        [SerializeField]
        private Transform tracksRoot;

        private Tween delayTween;

        private void Start()
        {
            CreateTracks();
        }

        private void CreateTracks()
        {
            foreach (Transform dot in dots)
            {
                Track newTrack = Instantiate(track, dot.position, dot.rotation, tracksRoot);
                newTrack.Fade();
            }
            delayTween = DOVirtual.DelayedCall(interval, CreateTracks, false);
        }

        private void OnDestroy()
        {
            delayTween?.Kill();
        }
    }
}
