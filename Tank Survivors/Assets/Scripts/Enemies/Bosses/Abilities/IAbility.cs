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
        public void EnableAbility();
        public void DisableAbility();
        public bool IsActive { get; }
    }
}
