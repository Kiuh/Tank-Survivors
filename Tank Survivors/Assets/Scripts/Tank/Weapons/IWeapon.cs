﻿using Common;

namespace Tank.Weapons
{
    [InterfaceEditor]
    public interface IWeapon : IUpgradablePiece
    {
        public void ProceedAttack();
    }
}
