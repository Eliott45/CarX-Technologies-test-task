using System;
using Extensions.PoolingSystem.Application;
using GameCore.Towers.Projectiles;
using UnityEngine;

namespace GameCore.Towers.Factory
{
    public class ProjectileFactory : IProjectilesFactory
    {
        private readonly IPoolApplication _poolApplication;

        public ProjectileFactory(IPoolApplication poolApplication)
        {
            _poolApplication = poolApplication ?? throw new NullReferenceException(nameof(IPoolApplication));
        }
        
        public IProjectile CreateProjectile<T>(T monsterPrefab, Transform spawnTransform) where T : Component, IProjectile => 
            _poolApplication.Create(monsterPrefab, spawnTransform);
    }
}