using UnityEngine;

namespace Extensions.PoolingSystem.Application
{
    public interface IPoolApplication
    {
        /// <summary>
        /// Get GameObject from pool.
        /// </summary>
        GameObject Create(GameObject prefab, Vector3 pos, Transform parent = null);

        /// <summary>
        /// Get component from pool.
        /// </summary>
        T Create<T>(T prefab, Transform parent = null) where T : Component;

        /// <summary>
        /// Return GameObject to pool.
        /// </summary>		
        void Return(GameObject createdObject);
    }
}