using GameCore.Enemies.Factory;
using GameCore.Enemies.Spawners;
using GameCore.Settings;
using UnityEngine;
using Zenject;

namespace GameCore.Enemies.Infrastructure
{
    public class EnemiesInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _monsterPrefab;
        [SerializeField] private MonsterSpawnerSettings _monsterSpawnerSettings;

        public override void InstallBindings()
        {
            BindSpawners();
            BindFactories();
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
        
        private void BindFactories()
        {
            Container
                .BindFactory<Vector3, Vector3, Monster, MonsterFactory>()
                .FromComponentInNewPrefab(_monsterPrefab);
        }
    }
}