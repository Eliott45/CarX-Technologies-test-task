using System;
using System.Linq;
using GameCore.Settings.Keywords;
using GameCore.Settings.Projectiles;
using GameCore.Settings.Towers;
using UnityEngine;

namespace GameCore.Settings.Repositories
{
    [CreateAssetMenu(fileName = nameof(TowerSettingsRepository), 
        menuName = "GameSettings/Repositories/" + nameof(TowerSettingsRepository))]
    public class TowerSettingsRepository : ScriptableObject
    {
        [SerializeField] private TowerSettings[] _towerSettings;

        public TowerSettings GetTowerSettings(ETowerKeyword keyword)
        {
            var settings = _towerSettings.FirstOrDefault(stg => stg.TowerKeyword == keyword);
            if (settings == null)
                throw new ArgumentException(
                    $"{nameof(ProjectileSettings)} for {keyword} projectile was not found!");

            return settings;
        }
    }
}