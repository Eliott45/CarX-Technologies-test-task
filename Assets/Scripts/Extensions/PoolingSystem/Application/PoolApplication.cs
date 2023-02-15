using System;
using Extensions.PoolingSystem.Controller;
using UnityEngine;

namespace Extensions.PoolingSystem.Application
{
    public class PoolApplication : IPoolApplication
    {
        private readonly IPoolController _poolController;
        
        public PoolApplication(IPoolController poolController)
        {
            _poolController = poolController ?? throw new NullReferenceException(nameof(IPoolController));
        }
        
        public GameObject Create(GameObject prefab, Vector3 pos, Transform parent = null) => 
            _poolController.CreateFromPool(prefab, pos, Quaternion.identity, parent);
        
        public T Create<T>(T prefab, Transform parent = null) where T : Component => 
            _poolController.CreateFromPool(prefab, parent);
        
        public void Return(GameObject createdObject) => 
            _poolController.ReturnToPool(createdObject);
    }
}