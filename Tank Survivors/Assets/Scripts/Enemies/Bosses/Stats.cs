using System;
using Sirenix.Serialization;

namespace Enemies.Bosses
{
    [Serializable]
    public class Stats
    {
        [OdinSerialize]
        public float Health { get; private set; }
    }
}
