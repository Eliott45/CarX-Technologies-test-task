using GameCore.Settings.Repositories;
using UnityEngine;
using Zenject;

namespace GameCore.Towers.Infrastructure
{
    public class TowersInstaller : MonoInstaller
    {
        [SerializeField] private TowerSettingsRepository _towerSettingsRepository;

        public override void InstallBindings()
        {
            Container
                .Bind<TowerSettingsRepository>()
                .FromInstance(_towerSettingsRepository)
                .AsSingle();
        }
    }
}