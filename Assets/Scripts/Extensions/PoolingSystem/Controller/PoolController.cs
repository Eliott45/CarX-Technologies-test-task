using System.Collections.Generic;
using UnityEngine;

namespace Extensions.PoolingSystem.Controller
{
    public class PoolController : MonoBehaviour, IPoolController
    {
        private class Pool
        {
            public readonly LinkedList<GameObject> inUse = new LinkedList<GameObject>();
            public readonly LinkedList<GameObject> noUse = new LinkedList<GameObject>();
        }
        
        [SerializeField] private int _defaultCount = 3;
        
        private readonly Dictionary<GameObject, Pool> _prefabToPool = new Dictionary<GameObject, Pool>();
        private readonly Dictionary<GameObject, Pool> _objectToPool = new Dictionary<GameObject, Pool>();
        
        private GameObject CreateFromPoolInternal(GameObject prefab, Transform holder)
        {
            if (!prefab)
                return null;

            var pool = GetOrCreatePool(prefab, _defaultCount);

            GameObject item;
            while (pool.noUse.Count > 0)
            {
                item = pool.noUse.Last.Value;
                pool.noUse.RemoveLast();
                if (!item) // destroyed?
                    continue;
                pool.inUse.AddLast(item);
                item.SetActive(true);

                return item;
            }

            // no items in pool => create new
            item = Instantiate(prefab, holder ? holder : transform);
            pool.inUse.AddLast(item);
            _objectToPool[item] = pool;

            return item;
        }
        
        public GameObject CreateFromPool(GameObject prefab, Vector3 pos, Quaternion rotation, Transform parent = null)
        {
            var obj = CreateFromPoolInternal(prefab.gameObject, parent);
            if (parent != null)
                obj.transform.SetParent(parent, false);
            obj.transform.position = pos;
            obj.transform.rotation = rotation;
            return obj;
        }
        
        public T CreateFromPool<T>(T prefab, Transform parent) where T : Component
        {
            var obj = CreateFromPoolInternal(prefab.gameObject, parent);
            obj.transform.SetParent(parent, false);
            return obj.GetComponent<T>();
        }
        
        private Pool GetOrCreatePool(GameObject prefab, int count)
        {
            if (_prefabToPool.TryGetValue(prefab, out var pool))
                return pool;

            _prefabToPool[prefab] = pool = new Pool();

            for (var i = 0; i < count; i++)
            {
                var obj = Instantiate(prefab, transform);
                obj.SetActive(false);
                pool.noUse.AddLast(obj);
                _objectToPool[obj] = pool;
            }

            return pool;
        }
        
        public void ReturnToPool(GameObject obj)
        {
            if (obj == null || !_objectToPool.TryGetValue(obj, out var pool))
                return;

            obj.transform.SetParent(transform, false);
            obj.SetActive(false);
            
            if (pool.inUse.Remove(obj)) 
                pool.noUse.AddLast(obj);
        }
    }
}
