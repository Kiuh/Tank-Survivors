using UnityEngine;

namespace Tank.Towers
{
    public class SingleShotTower : MonoBehaviour, ITower, ICanRotate
    {
        [SerializeField]
        private Transform shotPoint;

        public Vector3 GetShotPoint()
        {
            return shotPoint.position;
        }

        public void RotateTo(Vector2 direction)
        {
            transform.rotation = Quaternion.Euler(
                0f,
                0f,
                (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f
            );
        }
    }
}
