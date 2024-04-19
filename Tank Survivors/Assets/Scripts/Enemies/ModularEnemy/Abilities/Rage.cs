using System;
using System.Collections.Generic;
using Common;
using Enemies.Bosses.Abilities;
using Sirenix.OdinInspector;
using Tank;

namespace Enemies.Abilities
{
    [Serializable]
    [LabelText("Rage")]
    public class Rage : IAbility
    {
        public bool IsActive { get; set; }
        private Enemy enemy;
        private RageModule rage;
        private Shooting shooting;
        private float initialHealth;

        public List<IModule> GetModules()
        {
            return new() { new RageModule() };
        }

        public void Initialize(Enemy enemy, TankImpl tank)
        {
            this.enemy = enemy;
            rage = enemy.Modules.GetConcrete<RageModule, IModule>();
            shooting = enemy.Abilities.GetConcrete<Shooting, IAbility>();
            initialHealth = enemy
                .Modules.GetConcrete<HealthModule, IModule>()
                .Health.GetModifiedValue();
            IsActive = true;
        }

        public void Use()
        {
            if (rage.ScaleList.Count == 0)
            {
                return;
            }
            float healthPercentage = enemy.Health / initialHealth;
            if (rage.ScaleList[0].CooldownPercentage <= healthPercentage)
            {
                shooting.ShootingCooldown -=
                    shooting.ShootingCooldown * rage.ScaleList[0].CooldownPercentage;
                shooting.ShootingCooldown = Math.Max(
                    shooting.ShootingCooldown,
                    rage.MinimumCooldown
                );
                rage.ScaleList.RemoveAt(0);
            }
        }
    }
}
