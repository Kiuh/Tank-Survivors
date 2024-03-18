using Tank.Towers;
using UnityEngine;

namespace Tank.Weapons
{
    public enum SpawnVariation
    {
        Connected,
        Disconnected
    }

    public class ProjectileSpawner
    {
        private GunBase weapon;
        private ITower tower;

        public ProjectileSpawner(GunBase weapon, ITower tower)
        {
            this.weapon = weapon;
            this.tower = tower;
        }

        public void Spawn(SpawnVariation spawnVariation, Transform parent)
        {
            /*switch (spawnVariation)
            {
                case SpawnVariation.Connected:
                    return SpawnConnected(parent);
                case SpawnVariation.Disconnected:
                    return Spawn();
                default:
                    return null;
            }*/
        }
    }
}
