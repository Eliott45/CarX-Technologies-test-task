using Extensions.PoolingSystem.Application;
using UnityEngine;

namespace GameCore.Projectiles
{
    public interface IProjectile
    {
        void Init(IPoolApplication poolApplication);
        void SetTarget(GameObject gameObject);
        void SetDamage(int damage);
        void SetSpeed(float speed);
        void SetCurrentPosition(Vector3 spawnTransformPosition);
    }
}