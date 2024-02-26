using UnityEngine;

namespace Tank.Towers
{
    public class DoubleShotTower : MonoBehaviour, ITower, ICanRotate
    {
        [SerializeField]
        private Transform[] shotPoints = new Transform[2];

        private float rotationSpeed;
        private Quaternion targetRotation;

        private int currentShotPoint = 0;

        private void LateUpdate()
        {
            RotateInternal();
        }

        public Vector3 GetShotPoint()
        {
            currentShotPoint = (currentShotPoint + 1) % shotPoints.Length;

            return shotPoints[currentShotPoint].position;
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
