using GameCore.Projectiles.Interfaces;
using GameCore.Settings.Keywords;
using UnityEngine;

namespace GameCore.Projectiles.Factory
{
    public interface IProjectilesFactory
    {
        IProjectile CreateProjectile(EProjectileKeyword projectileKeyword, Transform spawnTransform = null);
    }
}