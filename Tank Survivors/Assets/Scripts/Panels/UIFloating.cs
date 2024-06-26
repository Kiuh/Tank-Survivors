using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Panels
{
    [AddComponentMenu("Scripts/Panels/Panels.UIFloating")]
    internal class UIFloating : MonoBehaviour
    {
        [Serializable]
        private struct RangeFloat
        {
            [MaxValue(nameof(Max))]
            public float Min;

            [MinValue(nameof(Min))]
            public float Max;

            public float GetRandom()
            {
                return UnityEngine.Random.Range(Min, Max);
            }
        }

        [Serializable]
        private struct RangeVector2
        {
            public Vector2 Min;
            public Vector2 Max;

            public Vector2 GetRandom()
            {
                Vector2 result;
                result.x = UnityEngine.Random.Range(Min.x, Max.x);
                result.y = UnityEngine.Random.Range(Min.y, Max.y);
                return result;
            }
        }

        [SerializeField]
        private RangeFloat delay;

        [SerializeField]
        private RangeVector2 positionRange;

        [ReadOnly]
        [SerializeField]
        private Vector3 startPosition;

        [ReadOnly]
        [SerializeField]
        private float duration;

        [ReadOnly]
        [SerializeField]
        private List<Vector3> path;
        private RectTransform rectTransform;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            startPosition = rectTransform.localPosition;
            duration = 0;
            path.Clear();
            for (int i = 0; i < 50; i++)
            {
                path.Add(startPosition + (Vector3)positionRange.GetRandom());
                duration += delay.GetRandom();
            }
            path.Add(path.First());
            Float();
        }

        private Tween moving;

        private void Float()
        {
            moving = rectTransform.DOLocalPath(
                path.ToArray(),
                duration,
                PathType.CubicBezier,
                PathMode.Full3D
            );
            _ = moving.onComplete += Float;
        }

        private void OnDestroy()
        {
            moving?.Kill();
        }
    }
}
