using UnityEngine;

namespace Sheet
{
    [RequireComponent(typeof(MeshRenderer))]
    [AddComponentMenu("Sheet.SheetScrolling")]
    public class SheetScrolling : MonoBehaviour
    {
        private Material material;

        private void Awake()
        {
            material = GetComponent<MeshRenderer>().material;
        }

        private void Update()
        {
            Vector2 newOffset =
                new(
                    transform.position.x / transform.localScale.x,
                    transform.position.y / transform.localScale.y
                );

            material.mainTextureOffset = newOffset;
        }
    }
}
