using GameCore.Settings.Projectiles;
using UnityEngine;

namespace GameCore.Projectiles.Factory
{
    public interface IProjectilesFactory
    {
        IProjectile CreateProjectile(EProjectileKeyword projectileKeyword, Transform spawnTransform);
    }
}