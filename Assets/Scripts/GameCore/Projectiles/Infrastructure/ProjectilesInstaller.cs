using GameCore.Projectiles.Factory;
using GameCore.Settings.Projectiles;
using UnityEngine;
using Zenject;

namespace GameCore.Projectiles.Infrastructure
{
    public class ProjectilesInstaller : MonoInstaller
    {
        [SerializeField] private ProjectileSettingsRepository projectileSettingsRepository;

        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<ProjectileFactory>()
                .AsSingle();
            
            Container
                .Bind<ProjectileSettingsRepository>()
                .FromInstance(projectileSettingsRepository)
                .AsSingle();
        }
    }
}