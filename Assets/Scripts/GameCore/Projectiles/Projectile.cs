using System;
using Extensions.PoolingSystem.Application;
using GameCore.Enemies;
using GameCore.Projectiles.Interfaces;
using UnityEngine;

namespace GameCore.Projectiles
{
    public abstract class Projectile : MonoBehaviour, IProjectile
    {
        [SerializeField] private protected Rigidbody rb;
        
        private IPoolApplication _poolApplication;
        
        private protected GameObject target;
        private protected float speed;
        
        private int _damage;

        public void Init(IPoolApplication poolApplication)
        {
            _poolApplication = poolApplication ?? throw new NullReferenceException(nameof(IPoolApplication));
        }

        public void SetTarget(GameObject targetGameObject) => 
            target = targetGameObject;
        
        public void SetDamage(int newDamage) => 
            _damage = newDamage;

        public void SetSpeed(float newSpeed) => 
            speed = newSpeed;

        public void Launch(Vector3 velocity, ForceMode forceMode = ForceMode.Force) => 
            rb.AddForce(velocity * speed, forceMode);

        private protected virtual void ReturnToPool() => 
            _poolApplication.Return(gameObject);

        private protected virtual void FixedUpdate()
        {
            if (target == null || !target.activeSelf) 
                ReturnToPool();
        }
        
        private void OnCollisionEnter(Collision potentialTarget)
        {
            if (!potentialTarget.gameObject.TryGetComponent<IEnemy>(out var enemy))
                return;
            
            enemy.TakeDamage(_damage);
            ReturnToPool();
        }
    }
}