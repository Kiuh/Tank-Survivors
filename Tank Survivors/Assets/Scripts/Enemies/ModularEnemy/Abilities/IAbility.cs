using System.Collections.Generic;
using Sirenix.OdinInspector;
using Tank;

namespace Enemies.Bosses.Abilities
{
    [HideReferenceObjectPicker]
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
