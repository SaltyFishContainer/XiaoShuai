using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lingdar77
{
    public sealed class ObjectPool : MonoBehaviour
    {
        [HideInInspector] public static ObjectPool Instance;
        private Dictionary<GameObject, Queue<GameObject>> map = new Dictionary<GameObject, Queue<GameObject>>();
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public GameObject GetObject(GameObject prototype)
        {
            GameObject result = null;
            if (!map.TryGetValue(prototype, out var queue) || queue.Count == 0)
            {
                result = Instantiate(prototype, transform);
                var comp = result.AddComponent<ObjectPoolChild>();
                comp.pool = this;
                comp.prototype = prototype;
            }
            else
            {
                result = queue.Dequeue();
            }
            result.transform.SetParent(null);
            result.SetActive(true);
            return result;
        }

        public void CacheObject(GameObject obj)
        {
            if (obj.TryGetComponent<ObjectPoolChild>(out var comp))
            {
                if (comp.pool != this)
                {
                    Object.Destroy(obj);
                    return;
                }
                if (!map.TryGetValue(comp.prototype, out var queue))
                {
                    queue = new Queue<GameObject>();
                    map.Add(comp.prototype, queue);
                }
                queue.Enqueue(obj);
                obj.transform.SetParent(transform);
                obj.SetActive(false);
            }
        }

        public void ClearPool()
        {
            foreach (var pair in map)
            {
                foreach (var obj in pair.Value)
                {
                    GameObject.Destroy(obj);
                }
            }
            map = new Dictionary<GameObject, Queue<GameObject>>();
        }
    }
}
