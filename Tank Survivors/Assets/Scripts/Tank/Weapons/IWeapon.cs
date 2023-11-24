using Sirenix.OdinInspector;
using System.Collections.Generic;
using Tank.UpgradablePiece;

namespace Tank.Weapons
{
    [HideReferenceObjectPicker]
    public interface IWeapon : IUpgradablePiece
    {
        public void ProceedAttack();
        public void Initialize(TankImpl tank, EnemyFinder enemyFinder);
        public abstract List<IWeaponModule> Modules { get; }
        public abstract void CreateGun();
    }
}
