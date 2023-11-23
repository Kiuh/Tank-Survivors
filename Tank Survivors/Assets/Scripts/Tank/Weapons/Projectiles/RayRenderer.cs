using System.Collections;
using UnityEngine;

namespace Tank.Weapons.Projectiles
{
    [RequireComponent(typeof(LineRenderer))]
    public class RayRenderer : MonoBehaviour, IProjectile
    {
        private LineRenderer lineRenderer;

        private float duration = 1f;
        private float timeRamaining;

        private Color startColor;
        private Color endColor;

        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        public void Initialize(float duration, Vector3 startPoint, Vector3 endPoint)
        {
            this.duration = duration;
            timeRamaining = duration;

            lineRenderer.SetPosition(0, startPoint);
            lineRenderer.SetPosition(1, endPoint);

            startColor = lineRenderer.startColor;
            endColor = lineRenderer.endColor;
        }

        public void Show()
        {
            _ = StartCoroutine(Disappear());
        }

        private IEnumerator Disappear()
        {
            lineRenderer.enabled = true;

            while (timeRamaining > 0f)
            {
                SetAlpha(timeRamaining / duration);

                yield return null;
                timeRamaining -= Time.deltaTime;
            }

            SetAlpha(0f);
            yield return null;

            lineRenderer.enabled = false;
            Destroy(gameObject);
        }

        private void SetAlpha(float a)
        {
            startColor.a = a;
            lineRenderer.startColor = startColor;
            endColor.a = a;
            lineRenderer.endColor = endColor;
        }
    }
}
