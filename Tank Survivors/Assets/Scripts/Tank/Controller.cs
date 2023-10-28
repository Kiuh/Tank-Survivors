using UnityEngine;

namespace Tank
{
    [AddComponentMenu("Tank.Controller")]
    public class Controller : MonoBehaviour
    {
        [SerializeField]
        private Moving moving;

        [SerializeField]
        private joystick.Joystick joystick;

        private void Update()
        {
            moving.MovementDirection = joystick.Direction;
        }
    }
}
