using Sirenix.OdinInspector;
using Tank;
using UnityEngine;

namespace Panels
{
    public class PlayerFollower : MonoBehaviour
    {
        public enum Offset
        {
            Scene,
            Value
        }

        [Required]
        [SerializeField]
        private RectTransform canvas;

        [Required]
        [SerializeField]
        private TankImpl tank;

        [SerializeField]
        [EnumToggleButtons]
        private Offset offsetType = Offset.Scene;

        [SerializeField]
        private Vector3 offset;

        private void Start()
        {
            if (offsetType == Offset.Scene)
            {
                offset = canvas.position - tank.transform.position;
            }
        }

        private void Update()
        {
            Vector3 position = tank.transform.position;
            canvas.position = position + offset;
        }
    }
}
