using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Panels
{
    [RequireComponent(typeof(Button))]
    [AddComponentMenu("Scripts/Panels/Panels.ColoredButtonHover")]
    internal class ColoredButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private Color hoveredColor = Color.yellow;

        [ReadOnly]
        [SerializeField]
        private Color normalColor;

        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
            normalColor = button.targetGraphic.color;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            button.targetGraphic.color = hoveredColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            button.targetGraphic.color = normalColor;
        }
    }
}
