using Sirenix.OdinInspector;
using UnityEngine;

namespace General
{
    [AddComponentMenu("Scripts/General/General.ParallaxBackground")]
    internal class ParallaxBackground : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private MeshRenderer meshRenderer;

        [Required]
        [SerializeField]
        private Transform targetTransform;

        [SerializeField]
        private float scrollSpeed = 1f;

        [SerializeField]
        private Vector3 startPosition;

        private void Start()
        {
            startPosition = transform.position;
        }

        private void Update()
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
            Vector2 offset = targetTransform.position - startPosition;
            meshRenderer.material.mainTextureOffset = offset * scrollSpeed;
        }
    }
}
