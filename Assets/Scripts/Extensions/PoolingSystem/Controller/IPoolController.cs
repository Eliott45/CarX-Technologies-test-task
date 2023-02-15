using UnityEngine;

namespace Extensions.PoolingSystem.Controller
{
    /// <summary>
    /// GameObject/Component Pool.
    /// Allows to create and reuse GameObject/Components from prefab.
    /// Drag this controller on any GameObject.
    /// </summary>
    public interface IPoolController
    {
        /// <summary>
        /// Get component from pool.
        /// </summary>
        T CreateFromPool<T>(T prefab, Transform parent) where T : Component;
        
        /// <summary>
        /// Get GameObject from pool.
        /// </summary>
        GameObject CreateFromPool(GameObject prefab, Vector3 pos, Quaternion rotation, Transform parent = null);
        
        /// <summary>
        /// Return GameObject to pool.
        /// </summary>
        void ReturnToPool(GameObject createdObject);
    }
}