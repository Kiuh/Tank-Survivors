﻿using System;
using System.Collections.Generic;
using Common;
using Enemies.Bosses.Abilities;
using Sirenix.OdinInspector;
using Tank;
using UnityEngine;

namespace Enemies.Abilities
{
    [Serializable]
    [LabelText("Dash")]
    public class Dash : IAbility
    {
        public enum States
        {
            Disabled,
            Preparing,
            Dashing
        }

        private States state;
        private Enemy enemy;
        private TankImpl tank;
        private Collider2D tankCollider;
        private DashModule dash;
        private Action executeState;
        private float timer;
        public bool IsActive { get; set; }

        public List<IModule> GetModules()
        {
            return new() { new DashModule() };
        }

        public void Initialize(Enemy enemy, TankImpl tank)
        {
            this.enemy = enemy;
            this.tank = tank;
            tankCollider = tank.GetComponent<BoxCollider2D>();
            dash = enemy.Modules.GetConcrete<DashModule, IModule>();
            enemy.FixedUpdatableAbilities.Add(this);
            timer = dash.CoolDown;
            IsActive = true;
            SetDisabledState();
        }

        public void Use()
        {
            timer -= Time.deltaTime;
            executeState?.Invoke();
        }

        public void UpdateState()
        {
            switch (state)
            {
                case States.Disabled:
                    SetPrepareState();
                    break;
                case States.Preparing:
                    SetDashingState();
                    break;
                case States.Dashing:
                    SetDisabledState();
                    break;
            }
        }

        private void SetDisabledState()
        {
            state = States.Disabled;
            timer = dash.CoolDown;
            SetAbilityActive(true);
            executeState = () =>
            {
                if (timer <= 0)
                {
                    UpdateState();
                }
            };
        }

        private void SetPrepareState()
        {
            state = States.Preparing;
            timer = dash.SlowPercent;
            Movement movement = enemy.Abilities.GetConcrete<Movement, IAbility>();
            float speed = 0.0f;
            if (movement != null)
            {
                speed = movement.Speed;
                movement.Speed *= 1 - dash.SlowPercent;
            }
            executeState = () =>
            {
                if (timer <= 0)
                {
                    UpdateState();
                    if (movement != null)
                    {
                        movement.Speed = speed;
                    }
                }
            };
        }

        private void SetDashingState()
        {
            state = States.Dashing;
            SetAbilityActive(false);
            MovementModule movement = enemy.Modules.GetConcrete<MovementModule, IModule>();

            float speed =
                movement == null
                    ? 0.0f
                    : movement.Speed.GetModifiedValue() * dash.DashSpeedMultiplier;

            float damage =
                enemy.Modules.GetConcrete<DamageModule, IModule>().Damage.GetModifiedValue() * 2.0f;

            Vector3 startPoint = enemy.transform.position;
            Vector2 endPoint =
                tank.transform.position + dash.CircleZone.GetRandomPoint() - startPoint;
            Vector2 direction = endPoint.normalized;
            float distance = endPoint.magnitude;
            Rigidbody2D rigidBody = enemy.RigidBody;

            executeState = () =>
            {
                if (rigidBody.IsTouching(tankCollider))
                {
                    tank.TakeDamage(speed);
                    UpdateState();
                }
                rigidBody.MovePosition(rigidBody.position + (direction * speed * Time.deltaTime));
                if ((rigidBody.transform.position - startPoint).magnitude >= distance)
                {
                    UpdateState();
                }
            };
        }

        private void SetAbilityActive(bool isActive)
        {
            DisableAbility(enemy.Abilities.GetConcrete<Movement, IAbility>(), isActive);
            DisableAbility(enemy.Abilities.GetConcrete<CloseRangeAttack, IAbility>(), isActive);
        }

        private void DisableAbility(IAbility ability, bool isActive)
        {
            if (ability != null)
            {
                ability.IsActive = isActive;
            }
        }
    }
}
