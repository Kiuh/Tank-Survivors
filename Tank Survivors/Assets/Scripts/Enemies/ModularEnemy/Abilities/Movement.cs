using System;
using System.Collections.Generic;
using Common;
using Enemies.Bosses.Abilities;
using Sirenix.OdinInspector;
using Tank;
using UnityEngine;

namespace Enemies.Abilities
{
    [Serializable]
    [LabelText("Movement")]
    public class Movement : IAbility
    {
        public float Speed { set; get; }
        private TankImpl tank;
        private Rigidbody2D rigidBody;
        private Enemy enemy;

        public bool IsActive { get; set; }

        public void Use()
        {
            Vector2 direction = CalculateDirection();
            rigidBody.MovePosition(rigidBody.position + (Speed * Time.fixedDeltaTime * direction));
            RotateToTank(direction);
        }

        private Vector3 CalculateDirection()
        {
            return (tank.transform.position - enemy.transform.position).normalized;
        }

        private void RotateToTank(Vector3 direction)
        {
            float rotationAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            enemy.transform.eulerAngles = Vector3.forward * -rotationAngle;
        }

        public void Initialize(Enemy enemy, TankImpl tank)
        {
            this.tank = tank;
            this.enemy = enemy;
            Speed = enemy.Modules.GetConcrete<MovementModule, IModule>().Speed.GetModifiedValue();
            rigidBody = enemy.RigidBody;
            enemy.FixedUpdatableAbilities.Add(this);
            IsActive = true;
        }

        public List<IModule> GetModules()
        {
            return new List<IModule>() { new MovementModule() };
        }
    }
}
