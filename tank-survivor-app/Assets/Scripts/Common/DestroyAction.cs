using Sirenix.OdinInspector;
using UnityEngine;

namespace Common
{
    [AddComponentMenu("Scripts/Common/Common.DestroyAction")]
    internal class DestroyAction : MonoBehaviour
    {
        [SerializeField]
        private GameObject toDestroy;

        [Button]
        public void DestroyGameObject()
        {
            Destroy(toDestroy);
        }
    }
}
