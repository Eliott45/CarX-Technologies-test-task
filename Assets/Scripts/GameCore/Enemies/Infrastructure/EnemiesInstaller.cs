using GameCore.Enemies.Factory;
using GameCore.Enemies.Spawners;
using GameCore.Settings;
using UnityEngine;
using Zenject;

namespace GameCore.Enemies.Infrastructure
{
    public class EnemiesInstaller : MonoInstaller
    {
        [SerializeField] private MonsterSpawnerSettings _monsterSpawnerSettings;

        public override void InstallBindings()
        {
            BindSpawners();
            
            Container
                .BindInterfacesTo<EnemyFactory>()
                .AsSingle();
        }

        private void BindSpawners()
        {
            Container
                .BindInterfacesTo<MonsterSpawner>()
                .AsSingle();
            
            Container
                .Bind<MonsterSpawnerSettings>()
                .FromInstance(_monsterSpawnerSettings)
                .AsSingle();
        }
    }
}