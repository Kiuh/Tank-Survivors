using UnityEngine;

namespace Tank.Towers
{
    public interface ITower
    {
        public Vector3 GetShotPoint();
    }

    public interface ICanRotate
    {
        public void RotateTo(RotationParameters parameters);
    }

    public struct RotationParameters
    {
        public float Speed;
        public Vector3 Direction;

        public RotationParameters(float speed, Vector3 direction)
        {
            Speed = speed;
            Direction = direction;
        }
    }
}
