using joystick;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tank
{
    [AddComponentMenu("Tank.Controller")]
    public class Controller : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private Moving moving;

        [Required]
        [SerializeField]
        private Joystick joystick;

        private void Update()
        {
            moving.MovementDirection = joystick.Direction;
        }
    }
}
