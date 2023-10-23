using Tank;
using UnityEngine;

namespace General.Configs
{
    [CreateAssetMenu(
        fileName = "TankStartProperties",
        menuName = "Configs/TankStartProperties",
        order = 4
    )]
    public class TankStartProperties : ScriptableObject
    {
        [SerializeField]
        private float health;
        public float Health => health;

        public void AssignStartProperties(TankImpl tank)
        {
            tank.Health.BaseValue = health;
        }
    }
}
