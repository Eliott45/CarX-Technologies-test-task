using Extensions.PoolingSystem.Application;
using UnityEngine;

namespace GameCore.Enemies
{
    public interface IEnemy
    {
        void Init(IPoolApplication poolApplication);
        
        void TakeDamage(int damage);
        
        void SetTargetPosition(Vector3 position);
        void SetMaxHp(int hp);
        void SetSpeed(float speed);
        GameObject GetGameObject();
    }
}