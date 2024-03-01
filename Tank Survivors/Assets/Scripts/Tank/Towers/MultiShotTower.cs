using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Tank.Towers
{
    public class MultiShotTower : MonoBehaviour, ITower
    {
        [SerializeField]
        private CannonPositioner cannonPositioner;

        [OdinSerialize]
        [SerializeField]
        [ValueDropdown("GetAllPositions")]
        private List<string> cannonPositions;
        public IEnumerable<string> CannonPositions => cannonPositions;

        private List<Cannon> cannons = new();
        private int currentShotPoint = 0;

        public int CannonsCount => cannons.Count;

        public Vector3 GetDirection()
        {
            return cannons[currentShotPoint].GetDirection();
        }

        public Vector3 GetShotPoint()
        {
            currentShotPoint = (currentShotPoint + 1) % cannons.Count;
            return cannons[currentShotPoint].GetShotPoint();
        }

        public void AddCannon(Cannon cannonPrefab, string id)
        {
            var cannonProperty = cannonPositioner.CannonProperties.First(p => p.Name.Equals(id));

            var cannon = Instantiate(
                cannonPrefab,
                cannonProperty.transform.position,
                cannonProperty.transform.rotation,
                transform
            );
            cannons.Add(cannon);
        }

        private IEnumerable GetAllPositions()
        {
            return cannonPositioner?.CannonProperties.Select(x => x.Name);
        }
    }
}
