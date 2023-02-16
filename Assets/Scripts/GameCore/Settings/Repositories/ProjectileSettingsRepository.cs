using System;
using System.Linq;
using GameCore.Settings.Keywords;
using GameCore.Settings.Projectiles;
using UnityEngine;

namespace GameCore.Settings.Repositories
{
    [CreateAssetMenu(fileName = nameof(ProjectileSettingsRepository), 
        menuName = "GameSettings/Repositories/" + nameof(ProjectileSettingsRepository))]
    public class ProjectileSettingsRepository : ScriptableObject
    {
        [SerializeField] private ProjectileSettings[] _projectileSettings;

        public ProjectileSettings GetProjectileSettings(EProjectileKeyword keyword)
        {
            var settings = _projectileSettings.FirstOrDefault(stg => stg.ProjectileKeyword == keyword);
            if (settings == null)
                throw new ArgumentException(
                    $"{nameof(ProjectileSettings)} for {keyword} projectile was not found!");

            return settings;
        }
    }
}