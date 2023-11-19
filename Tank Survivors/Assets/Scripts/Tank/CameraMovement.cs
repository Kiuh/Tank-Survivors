using UnityEngine;

namespace Tank.CameraMovement
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField]
        private TankImpl tank;

        private void Update()
        {
            transform.position = Vector3.Lerp(
                transform.position,
                new Vector3(tank.transform.position.x, tank.transform.position.y, -10),
                Time.deltaTime * tank.Speed.SourceValue
            );
        }
    }
}
