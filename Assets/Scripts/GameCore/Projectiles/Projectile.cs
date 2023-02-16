using System;
using Extensions.PoolingSystem.Application;
using UnityEngine;

namespace GameCore.Projectiles
{
    public abstract class Projectile : MonoBehaviour, IProjectile
    {
        [SerializeField] private protected Rigidbody rb;
        
        private IPoolApplication _poolApplication;
        
        private protected GameObject target;
        private protected float speed;
        private protected int damage;

        public void Init(IPoolApplication poolApplication)
        {
            _poolApplication = poolApplication ?? throw new NullReferenceException(nameof(IPoolApplication));
        }

        public void SetTarget(GameObject targetGameObject) => 
            target = targetGameObject;

        public void SetDamage(int newDamage) => 
            damage = newDamage;

        public void SetSpeed(float newSpeed) => 
            speed = newSpeed;
        
        protected void ReturnToPool() => 
            _poolApplication.Return(gameObject);
    }
}