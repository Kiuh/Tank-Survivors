using UnityEngine;

namespace Tank.Towers
{
    public interface ITower
    {
        public Vector3 GetShotPoint();
    }

    public interface ICanRotate
    {
        public void RotateTo(Vector2 direction);
    }
}
