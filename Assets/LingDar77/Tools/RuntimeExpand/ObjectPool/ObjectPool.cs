using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lingdar77.Singletons
{
    public sealed class ObjectPool
    {

        private GameObject root;

        public uint MaxCapacity = 100;
        private Dictionary<string, HashSet<GameObject>> pools = new Dictionary<string, HashSet<GameObject>>();

        private ObjectPool()
        {
            root = new GameObject("Pool Root");
        }
        public static ObjectPool Instance { get { return Nested.instance; } }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly ObjectPool instance = new ObjectPool();
        }

        public GameObject SpawnObject(GameObject orginal, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            HashSet<GameObject> pool;
            GameObject obj;
            if (!pools.TryGetValue(orginal.name, out pool))
            {
                pool = new HashSet<GameObject>();
                pools.Add(orginal.name, pool);
                obj = Object.Instantiate(orginal, position, rotation, parent);
                pool.Add(obj);
            }
            else
            {
                var i = pool.GetEnumerator();
                if (i.MoveNext())
                    obj = i.Current;
                else
                {
                    obj = Object.Instantiate(orginal, position, rotation, parent);
                    pool.Add(obj);
                    return obj;
                }
            }
            if (obj)
            {
                pool.Remove(obj);
                obj.transform.SetParent(parent);
                obj.transform.position = position;
                obj.transform.rotation = rotation;
                obj.SetActive(true);
                return obj;
            }

            // var obj = Object.Instantiate(orginal, position, rotation, parent);
            // Debug.Log(orginal.GetType());
            return null;
        }
        public void ReleaseObject(GameObject obj)
        {
            HashSet<GameObject> pool;
            var name = obj.name.Substring(0, obj.name.Length - 7);
            if (pools.TryGetValue(name, out pool))
            {
                pool.Add(obj);
                if (pool.Count > MaxCapacity)
                {
                    //try free objects when reach max capacity
                    root.transform.DetachChildren();
                    foreach (var i in pool)
                    {
                        Object.Destroy(i);
                    }
                    // Debug.Log("clear");
                    pool.Clear();
                }
            }
            else
            {
                pool = new HashSet<GameObject>();
                pools.Add(name, pool);
                pool.Add(obj);
            }
            obj.SetActive(false);
            obj.transform.SetParent(root.transform);
            // Debug.Log(root.transform.childCount);
        }

    }
}