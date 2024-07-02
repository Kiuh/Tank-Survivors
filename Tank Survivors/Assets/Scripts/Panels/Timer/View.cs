using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Panels.Timer
{
    public class View : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private TMP_Text timer;

        public void UpdateTime(string time)
        {
            timer.text = time;
        }
    }
}
