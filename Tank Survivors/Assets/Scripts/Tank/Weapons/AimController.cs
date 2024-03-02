using Tank.Towers;
using UnityEngine;

namespace Tank.Weapons
{
    public class AimController
    {
        private TankImpl tank;
        private GunBase weapon;
        private ICanRotate tower;

        public AimController(TankImpl tank, GunBase weapon, ICanRotate tower)
        {
            this.tank = tank;
            this.weapon = weapon;
            this.tower = tower;
        }

        public Vector3 GetAimDirection(Transform enemy)
        {
            return enemy.position - tank.transform.position;
        }

        public void Aim(Transform enemy)
        {
            tower.RotateTo(
                new RotationParameters()
                {
                    Direction = GetAimDirection(enemy),
                    Speed = weapon.GetModule<TowerRotationModule>().RotationSpeed.GetModifiedValue()
                }
            );
        }
    }
}
