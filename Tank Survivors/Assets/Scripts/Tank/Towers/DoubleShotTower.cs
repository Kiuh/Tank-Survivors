using UnityEngine;

namespace Tank.Towers
{
    public class DoubleShotTower : MonoBehaviour, ITower, ICanRotate
    {
        [SerializeField]
        private Transform[] shotPoints = new Transform[2];

        private int currentShotPoint = 0;

        public Vector3 GetShotPoint()
        {
            currentShotPoint = (currentShotPoint + 1) % shotPoints.Length;

            return shotPoints[currentShotPoint].position;
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
