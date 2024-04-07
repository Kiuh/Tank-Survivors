using System;
using System.Collections.Generic;
using Common;
using Sirenix.OdinInspector;
using Tank;
using UnityEngine;

namespace Enemies.Bosses.Abilities
{
    [Serializable]
    [LabelText("Movement")]
    public class Movement : IAbility
    {
        private MovementModule movement;
        private TankImpl tank;
        private Rigidbody2D rigidbody;
        private Enemy enemy;

        public bool IsActive { get; set; } = true;

        public void Use()
        {
            Vector2 direction = CalculateDirection();
            rigidbody.MovePosition(
                rigidbody.position + direction * movement.Speed.GetModifiedValue() * Time.deltaTime
            );
            RotatateToTank(direction);
        }

        private Vector3 CalculateDirection()
        {
            return (tank.transform.position - enemy.transform.position).normalized;
        }

        private void RotatateToTank(Vector3 direction)
        {
            float rotationAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            enemy.transform.eulerAngles = Vector3.forward * -rotationAngle;
        }

        public void Initialize(Enemy enemy, TankImpl tank, List<IModule> modules)
        {
            this.tank = tank;
            this.enemy = enemy;
            movement = modules.GetConcrete<MovementModule, IModule>();
            if (!enemy.TryGetComponent(out rigidbody))
            {
                SetRigidBody();
            }
            IsActive = true;
        }

        private void SetRigidBody()
        {
            _ = enemy.gameObject.AddComponent(typeof(Rigidbody2D));
            rigidbody = enemy.Rigidbody;
            rigidbody.gravityScale = 0.0f;
            rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;
        }

        public List<IModule> GetModules()
        {
            return new List<IModule>() { new MovementModule() };
        }
    }
}
