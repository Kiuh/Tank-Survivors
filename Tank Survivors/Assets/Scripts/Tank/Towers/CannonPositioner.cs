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
}
