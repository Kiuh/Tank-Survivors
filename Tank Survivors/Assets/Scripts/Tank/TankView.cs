using UnityEngine;

namespace Tank
{
    [AddComponentMenu("Tank.TankView")]
    public class TankView : MonoBehaviour
    {
        [SerializeField]
        private TankImpl tank;

        [SerializeField]
        private RectTransform healthBackground;

        [SerializeField]
        private RectTransform healthFrontBar;

        [SerializeField]
        private RectTransform experienceBackground;

        [SerializeField]
        private RectTransform experienceFrontBar;

        [Range(0, 1)]
        [SerializeField]
        private float lerpSpeed;

        private void Update()
        {
            LerpSize(
                healthFrontBar,
                healthBackground,
                tank.Health.Value,
                tank.Health.MaxValue,
                lerpSpeed
            );
            // TODO: for experience
        }

        private void LerpSize(
            RectTransform front,
            RectTransform back,
            float current,
            float max,
            float speed
        )
        {
            front.sizeDelta = Vector2.Lerp(
                front.sizeDelta,
                new Vector2(back.sizeDelta.x * (current / max), front.sizeDelta.y),
                speed
            );
        }
    }
}
