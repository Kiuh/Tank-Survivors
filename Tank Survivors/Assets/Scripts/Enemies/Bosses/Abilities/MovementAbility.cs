﻿using System;
using Sirenix.Serialization;
using Tank;
using UnityEngine;

namespace Enemies.Bosses.Abilities
{
    [Serializable]
    public class MovementAbility : IAbility
    {
        [OdinSerialize]
        private float speed;

        private TankImpl tank;
        private Boss boss;

        public bool IsActive { get; set; }

        public void Initialize(Boss boss, TankImpl tank)
        {
            this.boss = boss;
            this.tank = tank;
            IsActive = true;
        }

        public void ExecuteAbility()
        {
            if (IsActive)
            {
                Vector3 direction = CalculateDirection();
                boss.transform.position += direction * speed * Time.deltaTime;
                RotatateToTank(direction);
            }
        }

        public Vector3 CalculateDirection()
        {
            return (tank.transform.position - boss.transform.position).normalized;
        }

        public void RotatateToTank(Vector3 direction)
        {
            float rotationAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            boss.transform.eulerAngles = Vector3.forward * -rotationAngle;
        }
    }
}
