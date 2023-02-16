using UnityEngine;

namespace GameCore.Projectiles
{
    public interface IProjectile
    {
        void SetTarget(GameObject gameObject);
    }
}