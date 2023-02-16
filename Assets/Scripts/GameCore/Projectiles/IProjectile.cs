using UnityEngine;

namespace GameCore.Projectiles
{
    public interface IProjectile
    {
        void SetTarget(GameObject gameObject);
        void SetDamage(int damage);
        void SetSpeed(float speed);
    }
}