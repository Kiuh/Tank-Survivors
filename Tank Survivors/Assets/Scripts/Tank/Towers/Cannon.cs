using UnityEngine;

namespace Tank.Towers
{
    public class Cannon : MonoBehaviour
    {
        [SerializeField]
        private Transform shotPoint;

        public Vector3 GetShotPoint()
        {
            return shotPoint.position;
        }

        public Vector3 GetDirection()
        {
            return transform.up;
        }
    }
}
