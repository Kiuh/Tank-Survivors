using UnityEngine;

namespace Common
{
    [AddComponentMenu("Scripts/Common/Common.CameraFollower")]
    internal class CameraFollower : MonoBehaviour
    {
        [SerializeField]
        private Transform referenceTransform;

        [SerializeField]
        private float lerpSpeed;

        private void LateUpdate()
        {
            Vector3 newPosition =
                new(
                    referenceTransform.position.x,
                    referenceTransform.position.y,
                    transform.position.z
                );
            transform.position = Vector3.Lerp(
                transform.position,
                newPosition,
                Time.deltaTime * lerpSpeed
            );
        }
    }
}
