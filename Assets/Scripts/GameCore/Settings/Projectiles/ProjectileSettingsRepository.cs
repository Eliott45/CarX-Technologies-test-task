using System;
using System.Linq;
using UnityEngine;

namespace GameCore.Settings.Projectiles
{
    [CreateAssetMenu(fileName = nameof(ProjectileSettingsRepository), 
        menuName = "GameSettings/Projectiles/" + nameof(ProjectileSettingsRepository))]
    public class ProjectileSettingsRepository : ScriptableObject
    {
        [SerializeField] private ProjectileSettings[] _projectileSettings;

        public ProjectileSettings GetProjectileSettings(EProjectileKeyword keyword)
        {
            var settings = _projectileSettings.FirstOrDefault(cfg => cfg.ProjectileKeyword == keyword);
            if (settings == null)
                throw new ArgumentException(
                    $"{nameof(ProjectileSettings)} for {keyword} projectile was not found!");

            return settings;
        }
    }
}