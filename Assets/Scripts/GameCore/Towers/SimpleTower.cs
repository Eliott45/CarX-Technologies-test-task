using System;
using System.Collections.Generic;
using System.Linq;
using Extensions.PoolingSystem.Application;
using GameCore.Notifiers;
using UnityEngine;
using Zenject;

namespace GameCore.Towers
{
    public class SimpleTower : MonoBehaviour
    {
        [SerializeField] private float _attackReloading = 0.5f;
        [SerializeField] private float _attackRange = 4;
        [SerializeField] private float _heightIndent = 1.5f;
        [SerializeField] private EnemyNotifier enemyNotifier;
        [SerializeField] private GuidedProjectile _projectilePrefab;

        private IPoolApplication _poolApplication;

        private readonly List<GameObject> _targets = new List<GameObject>(5);
            
        private float _lastShotTime = float.NegativeInfinity; 

        [Inject]
        public void Construct(IPoolApplication poolApplication)
        {   
            _poolApplication = poolApplication ?? throw new NullReferenceException(nameof(IPoolApplication));;
        }

        private void Awake()
        {
            enemyNotifier.UpdateNotifierRadius(_attackRange);
            
            enemyNotifier.OnTargetEnter += OnEnemyAppear;
            enemyNotifier.OnTargetExit += OnEnemyDisappear;
        }
        
        private void Update () {
            
            if (_lastShotTime + _attackReloading > Time.time || !HasTarget())
                return;
            
            Attack(_targets.First());
        }

        private void OnDestroy()
        {
            enemyNotifier.OnTargetEnter -= OnEnemyAppear;
            enemyNotifier.OnTargetExit -= OnEnemyDisappear;
        }

        private void OnEnemyAppear(GameObject enemy) => 
            _targets.Add(enemy);

        private void OnEnemyDisappear(GameObject enemy) => 
            _targets.Remove(enemy);
        
        private bool HasTarget()
        {
            _targets.RemoveAll(x => !x);
            return _targets.Count > 0;
        } 
        
        private void Attack(GameObject target) 
        {
            var projectile = _poolApplication.Create(_projectilePrefab, transform);
            projectile.transform.position += Vector3.up * _heightIndent; 
            projectile.m_target = target; // TODO plug for example: SetTarget(target)
            
            _lastShotTime = Time.time;
        }
    }
}