using Sirenix.OdinInspector;
using UnityEngine;

namespace Panels.Timer
{
    public class Controller : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private General.Timer timer;

        [Required]
        [SerializeField]
        private View view;

        private void Update()
        {
            if (!timer.IsPaused)
            {
                view.UpdateTime(timer.FormattedCurrentTime);
            }
        }
    }
}
