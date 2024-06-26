using Sirenix.OdinInspector;
using UnityEngine;

namespace Tank.Towers.Cannon
{
    public class Cannon : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private Transform shotPoint;

        public Vector3 GetShotPoint()
        {
            return shotPoint.position;
        }

        public Vector3 GetDirection()
        {
            return shotPoint.up;
        }
    }
}
