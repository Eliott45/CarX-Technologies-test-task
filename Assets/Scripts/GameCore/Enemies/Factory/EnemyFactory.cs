using System;
using Extensions.PoolingSystem.Application;
using GameCore.Settings.Enemies;
using UnityEngine;

namespace GameCore.Enemies.Factory
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly IPoolApplication _poolApplication;
        private readonly MonsterSettings _monsterSettings;
        
        public EnemyFactory(IPoolApplication poolApplication, MonsterSettings monsterSettings)
        {
            _poolApplication = poolApplication ?? throw new NullReferenceException(nameof(IPoolApplication));
            _monsterSettings = monsterSettings 
                ? monsterSettings
                : throw new NullReferenceException(nameof(MonsterSettings));
        }

        public IEnemy CreateMonster(Monster monsterPrefab, Transform spawnTransform)
        {
            var monster = (IEnemy)_poolApplication.Create(monsterPrefab);

            monster.GetGameObject().transform.position = spawnTransform.position;
            monster.Init(_poolApplication);

            monster.SetMaxHp(_monsterSettings.Hp);
            monster.SetSpeed(_monsterSettings.Speed);

            return monster;
        }
    }
}