using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Tank
{
    [AddComponentMenu("Tank.TankView")]
    public class TankView : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private TankImpl tank;

        [Required]
        [SerializeField]
        private Image healthFrontBar;

        [Required]
        [SerializeField]
        private Image experienceFrontBar;

        [Range(0, 1)]
        [SerializeField]
        private float lerpSpeed;

        private void Update()
        {
            LerpBar(healthFrontBar, tank.Health.Value, tank.Health.MaxValue, lerpSpeed);

            LerpBar(
                experienceFrontBar,
                tank.PlayerLevel.ExperienceCount,
                tank.PlayerLevel.MaxExperienceCount,
                lerpSpeed
            );
        }

        private void LerpBar(Image front, float current, float max, float speed)
        {
            front.fillAmount = Mathf.Lerp(front.fillAmount, current / max, speed);
        }
    }
}
