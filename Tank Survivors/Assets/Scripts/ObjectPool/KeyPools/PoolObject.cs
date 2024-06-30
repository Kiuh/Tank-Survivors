using System;
using UnityEngine;

namespace Module.ObjectPool.KeyPools
{
    [Serializable]
    public struct PoolObject<T>
        where T : MonoBehaviour
    {
        public int PreloadCount;

        [SerializeField]
        public T Template;
    }
}
