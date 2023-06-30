using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lingdar77
{
    public class PoolTest : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        private List<GameObject> objs = new List<GameObject>();
        private ObjectPool pool;

        private void Awake()
        {
            pool = GetComponent<ObjectPool>();
        }
        public void Spawn()
        {
            objs.Add(pool.GetObject(prefab));
        }
        public void RandomDestroy()
        {
            var index = Random.Range(0, objs.Count);
            var obj = objs[index];
            obj.GetComponent<ObjectPoolChild>().CacheObject2Pool();
            objs.Remove(obj);
        }
    }
}
