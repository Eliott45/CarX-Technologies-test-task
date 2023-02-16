using GameCore.Towers.Projectiles;
using UnityEngine;

namespace GameCore.Towers.Factory
{
    public interface IProjectilesFactory
    {
        IProjectile CreateProjectile<T>(T monsterPrefab, Transform spawnTransform) where T : Component, IProjectile;
    }
}