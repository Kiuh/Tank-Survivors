using TMPro;
using UnityEngine;

namespace Panels.Timer
{
    public class View : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI timer;

        public void UpdateTime(float time)
        {
            int minutes = (int)time / 60;
            int seconds = (int)time % 60;
            timer.text = $"{minutes}:{seconds}";
        }
    }
}
