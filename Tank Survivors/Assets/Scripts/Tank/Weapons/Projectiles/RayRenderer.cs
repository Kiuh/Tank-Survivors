using Enemies;
using System.Collections;
using UnityEngine;

namespace Tank.Weapons.Projectiles
{
    [RequireComponent(typeof(LineRenderer))]
    public class RayRenderer : MonoBehaviour, IProjectile
    {
        private LineRenderer lineRenderer;

        private float damage;

        private float duration = 1f;
        private float timeRamaining;

        private Color startColor;
        private Color endColor;

        private Vector3 startPoint;
        private Vector3 endPoint;

        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        public void Initialize(float damage, float duration, Vector3 startPoint, Vector3 endPoint)
        {
            this.damage = damage;

            this.duration = duration;
            timeRamaining = duration;

            this.startPoint = startPoint;
            this.endPoint = endPoint;

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

            Vector3 direction = endPoint - startPoint;
            float distance = (endPoint - startPoint).magnitude;

            while (timeRamaining > 0f)
            {
                SetAlpha(timeRamaining / duration);

                RaycastHit2D[] collisions = Physics2D.RaycastAll(startPoint, direction, distance);

                foreach (RaycastHit2D collision in collisions)
                {
                    if (collision.transform.TryGetComponent(out IEnemy enemy))
                    {
                        enemy.TakeDamage(damage);
                    }
                }

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
