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
        private float speed;
        private TankImpl tank;
        private Rigidbody2D rigidbody;
        private Enemy enemy;

        public bool IsActive { get; set; }

        public void Use()
        {
            Vector2 direction = CalculateDirection();
            rigidbody.MovePosition(rigidbody.position + speed * Time.fixedDeltaTime * direction);
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

        public void Initialize(Enemy enemy, TankImpl tank)
        {
            this.tank = tank;
            this.enemy = enemy;
            speed = enemy.Modules.GetConcrete<MovementModule, IModule>().Speed.GetModifiedValue();
            rigidbody = enemy.Rigidbody;
            enemy.FixedUpdatableAbilities.Add(this);
            IsActive = true;
        }

        public List<IModule> GetModules()
        {
            return new List<IModule>() { new MovementModule() };
        }
    }
}
