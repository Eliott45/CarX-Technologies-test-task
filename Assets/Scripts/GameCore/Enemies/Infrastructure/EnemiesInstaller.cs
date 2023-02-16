using GameCore.Enemies.Factory;
using GameCore.Enemies.Spawners;
using GameCore.Settings.Enemies;
using UnityEngine;
using Zenject;

namespace GameCore.Enemies.Infrastructure
{
    public class EnemiesInstaller : MonoInstaller
    {
        [SerializeField] private MonsterSpawnerSettings _monsterSpawnerSettings;
        [SerializeField] private MonsterSettings _monsterSettings;

        public override void InstallBindings()
        {
            BindSpawners();
            BindFactory();
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

        private void BindFactory()
        {
            Container
                .BindInterfacesTo<EnemyFactory>()
                .AsSingle();
            
            Container
                .Bind<MonsterSettings>()
                .FromInstance(_monsterSettings)
                .AsSingle();
        }
    }
}