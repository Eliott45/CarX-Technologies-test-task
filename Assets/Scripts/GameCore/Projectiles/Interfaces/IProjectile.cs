using Extensions.PoolingSystem.Application;
using UnityEngine;

namespace GameCore.Projectiles.Interfaces
{
    public interface IProjectile
    {
        void Init(IPoolApplication poolApplication);
        void SetTarget(GameObject gameObject);
        void SetDamage(int damage);
        void SetSpeed(float speed);
        void Launch(Vector3 velocity, ForceMode forceMode);
    }
}