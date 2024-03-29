﻿using GameCore.Projectiles.Factory;
using GameCore.Settings.Repositories;
using UnityEngine;
using Zenject;

namespace GameCore.Projectiles.Infrastructure
{
    public class ProjectilesInstaller : MonoInstaller
    {
        [SerializeField] private ProjectileSettingsRepository _projectileSettingsRepository;

        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<ProjectileFactory>()
                .AsSingle();
            
            Container
                .Bind<ProjectileSettingsRepository>()
                .FromInstance(_projectileSettingsRepository)
                .AsSingle();
        }
    }
}