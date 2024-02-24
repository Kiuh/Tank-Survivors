using Sirenix.OdinInspector;
using Tank;
using UnityEngine;

namespace Enemies.Bosses.Abilities
{
    [HideReferenceObjectPicker]
    public interface IAbility
    {
        public void Initialize(Boss boss, TankImpl tank);
        public void Execute();
        public void Enable()
        {
            IsActive = true;
        }
        public void Disable()
        {
            IsActive = false;
        }
        public bool IsActive { get; set; }
    }
}
