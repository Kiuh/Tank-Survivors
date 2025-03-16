using System;
using System.Collections.Generic;
using Common;
using Enemies.Bosses.Abilities;
using Enemies.ModularEnemy.Abilities;
using Sirenix.OdinInspector;
using Tank;
using UnityEngine;

namespace Enemies.Abilities
{
    [Serializable]
    [LabelText("Dash")]
    public class Dash : IAbility
    {
        public enum State
        {
            Disabled,
            Preparing,
            Dashing
        }

        private State state;
        private Enemy enemy;
        private TankImpl tank;
        private Collider2D tankCollider;
        private DashModule dashModule;
        private Action stateAction;
        private float timer;
        private DashLine dashLine;
        private float dashLineLength;
        private Vector3 randomDashPoint;
        private Vector3 startPoint;
        private Vector3 endPoint;
        public bool IsActive { get; set; }
        public State InternalState
        {
            get => state;
            private set
            {
                state = value;
                StateChanged?.Invoke(value);
            }
        }
        public event Action<State> StateChanged;

        public List<IModule> GetModules()
        {
            return new() { new DashModule() };
        }

        public void Initialize(Enemy enemy, TankImpl tank)
        {
            this.enemy = enemy;
            this.tank = tank;
            tankCollider = tank.GetComponent<BoxCollider2D>();
            dashModule = enemy.Modules.GetConcrete<DashModule, IModule>();
            enemy.UpdatableAbilities.Add(this);
            timer = dashModule.CoolDown;
            IsActive = true;
            dashLine = enemy.transform.GetComponentInChildren<DashLine>();
            dashLine.RefreshLine();
            SetDisabledState();
        }

        public void Use()
        {
            if (InternalState == State.Preparing)
            {
                float norm = 1 - (timer / dashModule.AimTime);
                dashLine.SetLength(norm * dashLineLength);
                dashLine.SetAlpha(norm);
            }
            timer -= Time.deltaTime;
            stateAction?.Invoke();
        }

        public void SwitchState()
        {
            switch (InternalState)
            {
                case State.Disabled:
                    SetPrepareState();
                    break;
                case State.Preparing:
                    dashLine.RefreshLine();
                    SetDashingState();
                    break;
                case State.Dashing:
                    SetDisabledState();
                    break;
            }
        }

        private void SetDisabledState()
        {
            InternalState = State.Disabled;
            timer = dashModule.CoolDown;
            SetAbilityActive(true);
            stateAction = () =>
            {
                if (timer <= 0)
                {
                    SwitchState();
                }
            };
        }

        private void SetPrepareState()
        {
            InternalState = State.Preparing;
            timer = dashModule.AimTime;
            Movement movement = enemy.Abilities.GetConcrete<Movement, IAbility>();
            movement.IsRotating = false;
            float speed = movement.Speed;
            movement.Speed *= 1 - dashModule.SlowPercent;

            startPoint = enemy.transform.position;
            randomDashPoint = dashModule.CircleZone.GetRandomPoint();
            endPoint = tank.transform.position + randomDashPoint;
            dashLineLength = ((Vector2)(endPoint - startPoint)).magnitude;

            stateAction = () =>
            {
                if (timer <= 0)
                {
                    movement.IsRotating = true;
                    movement.Speed = speed;
                    SwitchState();
                }
            };
        }

        private void SetDashingState()
        {
            InternalState = State.Dashing;
            SetAbilityActive(false);

            Movement movement = enemy.Abilities.GetConcrete<Movement, IAbility>();
            float speed = movement.Speed * dashModule.DashSpeedMultiplier;

            float damage =
                enemy.Modules.GetConcrete<DamageModule, IModule>().Damage.GetModifiedValue()
                * dashModule.DamageMultiplier;

            Vector2 direction = ((Vector2)(endPoint - startPoint)).normalized;
            Rigidbody2D rigidBody = enemy.RigidBody;

            stateAction = () =>
            {
                if (rigidBody.IsTouching(tankCollider))
                {
                    tank.TakeDamage(damage);
                    SwitchState();
                }
                rigidBody.MovePosition(rigidBody.position + (direction * speed * Time.deltaTime));
                if ((rigidBody.transform.position - startPoint).magnitude >= dashLineLength)
                {
                    SwitchState();
                }
            };
        }

        private void SetAbilityActive(bool isActive)
        {
            enemy.Abilities.GetConcrete<Movement, IAbility>().IsActive = isActive;
            enemy.Abilities.GetConcrete<CloseRangeAttack, IAbility>().IsActive = isActive;
        }
    }
}
