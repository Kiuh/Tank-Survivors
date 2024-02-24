using Sirenix.OdinInspector;
using Tank;
using UnityEngine;

namespace Enemies.Bosses.Abilities
{
    [HideReferenceObjectPicker]
    public interface IAbility
    {
        public void Initialize(Boss boss, TankImpl tank);
        public void ExecuteAbility();
        public void EnableAbility()
        {
            IsActive = true;
        }
        public void DisableAbility()
        {
            IsActive = false;
        }
        public bool IsActive { get; set; }
    }
}
