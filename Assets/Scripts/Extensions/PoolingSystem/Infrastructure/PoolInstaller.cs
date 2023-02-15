using Extensions.PoolingSystem.Application;
using Extensions.PoolingSystem.Controller;
using UnityEngine;
using Zenject;

namespace Extensions.PoolingSystem.Infrastructure
{
    public class PoolInstaller : MonoInstaller
    {
        [SerializeField] private PoolController _poolController;
        
        public override void InstallBindings()
        {
            Container
                .Bind<IPoolController>()
                .FromInstance(_poolController)
                .AsSingle();

            Container
                .Bind<IPoolApplication>()
                .To<PoolApplication>()
                .AsSingle();
        }
    }
}