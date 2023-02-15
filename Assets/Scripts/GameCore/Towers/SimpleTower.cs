using System;
using Extensions.PoolingSystem.Application;
using UnityEngine;
using Zenject;

namespace GameCore.Towers
{
    public class SimpleTower : MonoBehaviour
    {
        [SerializeField] private float _attackDelay;
        [SerializeField] private float _attackRange;
        [SerializeField] private float _heightIndent = 1.5f;
        [SerializeField] private SphereCollider _triggerCollider;
        [SerializeField] private GuidedProjectile _projectilePrefab;

        private IPoolApplication _poolApplication;
        
        private float _lastShotTime = -0.5f;

        [Inject]
        public void Construct(IPoolApplication poolApplication)
        {   
            _poolApplication = poolApplication;
        }

        private void Awake()
        {
            UpdateTriggerColliderRadius(_attackRange);
        }

        private void Update () {
            Attack();
        }

        private void UpdateTriggerColliderRadius(float radius) => 
            _triggerCollider.radius = radius;

        private void Attack()
        {
            foreach (var monster in FindObjectsOfType<Monster>()) {
                if (Vector3.Distance (transform.position, monster.transform.position) > _attackRange)
                    continue;

                if (_lastShotTime + _attackDelay > Time.time)
                    continue;
                
                var projectile = _poolApplication.Create(_projectilePrefab, transform);
                projectile.transform.position += Vector3.up * _heightIndent; 
                projectile.m_target = monster.gameObject; // TODO pls

                _lastShotTime = Time.time;
            }
        }
    }
}