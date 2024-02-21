﻿using System;
using Sirenix.Serialization;

namespace Enemies.Bosses
{
    [Serializable]
    public class BossStats
    {
        [OdinSerialize]
        public float Health { get; private set; }
    }
}
