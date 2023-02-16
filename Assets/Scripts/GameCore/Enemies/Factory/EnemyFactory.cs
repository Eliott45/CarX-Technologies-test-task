using System;
using Extensions.PoolingSystem.Application;
using UnityEngine;

namespace GameCore.Enemies.Factory
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly IPoolApplication _poolApplication;
        
        public EnemyFactory(IPoolApplication poolApplication)
        {
            _poolApplication = poolApplication ?? throw new NullReferenceException(nameof(IPoolApplication));
        }

        public IEnemy CreateMonster(Monster monsterPrefab, Transform spawnPosition) => 
            _poolApplication.Create(monsterPrefab, spawnPosition);
    }
}