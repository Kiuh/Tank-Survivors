using System.Collections.Generic;
using UnityEngine;

namespace Tank.Towers.Cannon
{
    public class Positioner : MonoBehaviour
    {
        [SerializeField]
        private List<Property> properties = new();
        public IEnumerable<Property> Properties => properties;
    }
}
