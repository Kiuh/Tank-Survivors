using System.Collections.Generic;
using DG.Tweening;
using Module.ObjectPool;
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

        private MonoBehObjectPool<Track> trackPool;

        private void Start()
        {
            trackPool = new MonoBehObjectPool<Track>(track, 0, tracksRoot);
            CreateTracks();
        }

        private void CreateTracks()
        {
            foreach (Transform dot in dots)
            {
                Track newTrack = trackPool.Get();
                newTrack.transform.SetPositionAndRotation(dot.position, dot.rotation);
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
