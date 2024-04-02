using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Tank;
using UnityEngine;

namespace Panels
{
    public class PlayerFollower : SerializedMonoBehaviour
    {
        public enum Offset
        {
            scene,
            value
        }

        [OdinSerialize]
        private RectTransform canvas;

        [OdinSerialize]
        private TankImpl tank;

        [OdinSerialize]
        [EnumToggleButtons]
        private Offset offsetType = Offset.scene;

        [OdinSerialize]
        private Vector3 offset;

        private void Start()
        {
            if (offsetType == Offset.scene)
            {
                offset = canvas.position - tank.transform.position;
            }
        }

        private void Update()
        {
            Vector3 position = tank.transform.position;
            canvas.position = position + offset;
        }
    }
}
