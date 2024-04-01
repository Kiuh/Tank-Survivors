using System.Collections.Generic;
using Sirenix.OdinInspector;
using Tank.UpgradablePiece;
using Tank.Weapons.Modules;

namespace Tank.Weapons
{
    [HideReferenceObjectPicker]
    public interface IWeapon : IUpgradablePiece
    {
        public void ProceedAttack();
        public void Initialize(TankImpl tank, EnemyFinder enemyFinder);
        public abstract List<IWeaponModule> Modules { get; }
        public IEnumerable<ILevelUpUpgrade> LevelUpUpgrades { get; }
        public abstract void CreateGun();
        public abstract void DestroyGun();
        public abstract void SwapWeapon(IWeapon newWeapon);
    }
}
