using System;
using Extensions.PoolingSystem.Application;
using GameCore.Projectiles.Interfaces;
using GameCore.Settings.Keywords;
using GameCore.Settings.Projectiles;
using GameCore.Settings.Repositories;
using UnityEngine;

namespace GameCore.Projectiles.Factory
{
    public class ProjectileFactory : IProjectilesFactory
    {
        private readonly IPoolApplication _poolApplication;
        private readonly ProjectileSettingsRepository _projectileSettingsRepository;

        public ProjectileFactory(IPoolApplication poolApplication, ProjectileSettingsRepository projectileSettingsRepository)
        {
            _poolApplication = poolApplication ?? throw new NullReferenceException(nameof(IPoolApplication));
            _projectileSettingsRepository = projectileSettingsRepository 
                ? projectileSettingsRepository
                : throw new NullReferenceException(nameof(ProjectileSettingsRepository));
        }

        public IProjectile CreateProjectile(EProjectileKeyword projectileKeyword, Transform spawnTransform = null)
        {
            var projectileSettings = _projectileSettingsRepository.GetProjectileSettings(projectileKeyword);
            
            var projectile = (IProjectile)_poolApplication.Create(projectileSettings.ProjectilePrefab, spawnTransform);
            
            projectile.Init(_poolApplication);
            
            projectile.SetSpeed(projectileSettings.SpeedMultiplier);
            projectile.SetDamage(projectileSettings.Damage);

            return projectile;
        }
    }
}