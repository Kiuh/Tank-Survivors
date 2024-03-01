using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tank.Towers
{
    public class CannonPositioner : MonoBehaviour
    {
        [SerializeField]
        private List<CannonPosition> cannonProperties = new();
        public IEnumerable<CannonPosition> CannonProperties => cannonProperties;
    }

    [Serializable]
    public class CannonPosition : MonoBehaviour
    {
        [SerializeField]
        private string positionName;
        public string Name => positionName;

        private float vectorLength = 0.2f;

        private void OnDrawGizmos()
        {
            var rotatedVector =
                Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * Vector3.up;

            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, rotatedVector * vectorLength);
        }
    }
}
