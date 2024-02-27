using UnityEngine;

namespace General
{
    public class EnemyProgressor : MonoBehaviour
    {
        [SerializeField]
        private Configs.Enemies enemies;

        [SerializeField]
        private Timer timer;

        public void Awake()
        {
            //todo init
        }

        public void Update()
        {
            if (!timer.IsPaused)
            {
                //todo update stats
            }
        }
    }
}
