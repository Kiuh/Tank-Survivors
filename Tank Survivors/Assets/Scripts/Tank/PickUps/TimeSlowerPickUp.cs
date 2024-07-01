using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tank.PickUps
{
    [AddComponentMenu("Tank.PickUps.TimeSlowerPickUp")]
    public class TimeSlowerPickUp : MonoBehaviour, IPickUp
    {
        [SerializeField]
        private float time;

        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float timeSlowerPercentage;

        [SerializeField]
        private string effectText;

        [SerializeField]
        private Color effectColor;

        [Required]
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private bool grabbed;
        public bool Grabbed => grabbed;

        [SerializeField]
        private string pickupName;
        public string PickupName => pickupName;

        [Required]
        [SerializeField]
        private FloatingEffect floatingEffect;

        [ReadOnly]
        [SerializeField]
        private TankImpl tank;

        [SerializeField]
        private float followSpeed;

        [SerializeField]
        private float timeToSelfDestroy;
        private Tween selfDestroyTween;

        private void Start()
        {
            selfDestroyTween = DOVirtual.DelayedCall(
                timeToSelfDestroy,
                () => Destroy(gameObject),
                false
            );
        }

        private void OnDestroy()
        {
            selfDestroyTween?.Kill();
        }

        public void Grab(TankImpl tank)
        {
            grabbed = true;
            this.tank = tank;
        }

        private IEnumerator StartTimeSLower()
        {
            Time.timeScale = timeSlowerPercentage;
            FloatingEffect effect = Instantiate(
                floatingEffect,
                transform.position,
                Quaternion.identity
            );
            effect.Launch(effectText, effectColor);
            yield return new WaitForSecondsRealtime(time);
            Time.timeScale = 1.0f;
            Destroy(gameObject);
        }

        private void Update()
        {
            if (grabbed)
            {
                transform.position = Vector3.Lerp(
                    transform.position,
                    tank.transform.position,
                    Time.deltaTime * followSpeed
                );
            }
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out TankImpl tank))
            {
                CompleteGrab(tank);
            }
        }

        private void CompleteGrab(TankImpl tank)
        {
            _ = StartCoroutine(StartTimeSLower());
            spriteRenderer.enabled = false;
        }
    }
}
