using UnityEngine;

namespace Panels.Timer
{
    public class Controller : MonoBehaviour
    {
        [SerializeField]
        private General.Timer timer;

        [SerializeField]
        private View view;

        private void Update()
        {
            if (!timer.IsPaused)
            {
                view.UpdateTime(timer.CurrentTime);
            }
        }
    }
}
