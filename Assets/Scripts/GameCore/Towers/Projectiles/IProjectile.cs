using UnityEngine;

namespace GameCore.Towers.Projectiles
{
    public interface IProjectile
    {
        void SetTarget(GameObject gameObject);
    }
}