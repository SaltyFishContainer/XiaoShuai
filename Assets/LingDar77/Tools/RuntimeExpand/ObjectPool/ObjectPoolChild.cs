using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lingdar77
{
    public class ObjectPoolChild : MonoBehaviour
    {
        public ObjectPool pool;
        public GameObject prototype;

        public void CacheObject2Pool()
        {
            if (pool)
            {
                pool.CacheObject(gameObject);
            }
        }
    }
}
