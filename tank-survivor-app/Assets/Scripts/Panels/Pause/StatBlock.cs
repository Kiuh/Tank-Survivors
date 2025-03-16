using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Panels.Pause
{
    [AddComponentMenu("Scripts/Panels/Pause/Panels.Pause.StatBlock")]
    internal class StatBlock : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private TMP_Text nameLabel;

        [Required]
        [SerializeField]
        private TMP_Text valueLabel;

        public void SetContent(string name, string value)
        {
            nameLabel.text = name;
            valueLabel.text = value;
        }
    }
}
