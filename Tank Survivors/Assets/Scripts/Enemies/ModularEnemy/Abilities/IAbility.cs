using System.Collections.Generic;
using Tank;

namespace Enemies.Bosses.Abilities
{
    public interface IAbility
    {
        public void Initialize(Enemy enemy, TankImpl tank);
        public void Use();
        public void Enable()
        {
            IsActive = true;
        }
        public void Disable()
        {
            IsActive = false;
        }
        public bool IsActive { get; set; }
        public List<IModule> GetModules();
    }
}
