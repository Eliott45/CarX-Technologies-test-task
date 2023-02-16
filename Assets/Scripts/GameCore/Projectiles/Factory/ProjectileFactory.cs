using System;
using Extensions.PoolingSystem.Application;
using GameCore.Settings.Projectiles;
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

        public IProjectile CreateProjectile(EProjectileKeyword projectileKeyword, Transform spawnTransform)
        {
            var projectileSettings = _projectileSettingsRepository.GetProjectileSettings(projectileKeyword);
            
            var projectile = (IProjectile)_poolApplication.Create(projectileSettings.ProjectilePrefab, spawnTransform);
            
            projectile.Init(_poolApplication);
            projectile.SetCurrentPosition(spawnTransform.position);
            
            projectile.SetSpeed(projectileSettings.Speed);
            projectile.SetDamage(projectileSettings.Damage);

            return projectile;
        }
    }
}