using UnityEngine;

namespace Tank.Towers
{
    public class SingleShotTower : MonoBehaviour, ITower, ICanRotate
    {
        [SerializeField]
        private Transform shotPoint;

        private float rotationSpeed;
        private Quaternion targetRotation;

        private void LateUpdate()
        {
            RotateInternal();
        }

        public Vector3 GetShotPoint()
        {
            return shotPoint.position;
        }

        public void RotateTo(Vector2 direction, float rotationSpeed)
        {
            targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
            this.rotationSpeed = rotationSpeed;
        }

        private void RotateInternal()
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                Time.deltaTime * rotationSpeed
            );
        }
    }
}
