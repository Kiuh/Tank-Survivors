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

        public Vector3 GetDirection()
        {
            return transform.up;
        }

        public void RotateTo(RotationParameters parameters)
        {
            targetRotation = Quaternion.LookRotation(Vector3.forward, parameters.Direction);
            rotationSpeed = parameters.Speed;
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
