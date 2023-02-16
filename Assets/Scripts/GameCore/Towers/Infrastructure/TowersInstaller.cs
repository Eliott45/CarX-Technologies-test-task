using GameCore.Settings.Towers;
using GameCore.Towers.Factory;
using UnityEngine;
using Zenject;

namespace GameCore.Towers.Infrastructure
{
    public class TowersInstaller : MonoInstaller
    {
        [SerializeField] private SimpleTowerSettings _simpleTowerSettings;

        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<ProjectileFactory>()
                .AsSingle();
            
            Container
                .Bind<SimpleTowerSettings>()
                .FromInstance(_simpleTowerSettings)
                .AsSingle();
        }
    }
}