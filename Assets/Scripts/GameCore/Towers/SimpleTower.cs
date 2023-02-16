using System;
using System.Collections.Generic;
using System.Linq;
using Extensions.PoolingSystem.Application;
using GameCore.Notifiers;
using GameCore.Settings.Towers;
using UnityEngine;
using Zenject;

namespace GameCore.Towers
{
    public class SimpleTower : MonoBehaviour
    {
        [SerializeField] private EnemyNotifier _enemyNotifier;

        private readonly List<GameObject> _targets = new List<GameObject>(5);
        
        private IPoolApplication _poolApplication;
        private SimpleTowerSettings _settings;
        private float _lastShotTime = float.NegativeInfinity;

        [Inject]
        public void Construct(IPoolApplication poolApplication, SimpleTowerSettings settings)
        {   
            _poolApplication = poolApplication ?? throw new NullReferenceException(nameof(IPoolApplication));
            _settings = settings 
                ? settings 
                : throw new NullReferenceException(nameof(SimpleTowerSettings));
        }

        private void OnEnable()
        {
            _enemyNotifier.OnTargetEnter += OnEnemyAppear;
            _enemyNotifier.OnTargetExit += OnEnemyDisappear;
        }

        private void OnDisable()
        {
            _enemyNotifier.OnTargetEnter -= OnEnemyAppear;
            _enemyNotifier.OnTargetExit -= OnEnemyDisappear;
        }

        private void Awake()
        {
            _enemyNotifier.UpdateNotifierRadius(_settings.AttackRange);
        }
        
        private void Update () {
            
            if (!IsAvailableToAttack())
                return;
            
            Attack(_targets.First());
        }

        private bool IsAvailableToAttack() => 
            !(_lastShotTime + _settings.AttackReloading > Time.time) && HasTarget();

        private void OnEnemyAppear(GameObject enemy) => 
            _targets.Add(enemy);

        private void OnEnemyDisappear(GameObject enemy) => 
            _targets.Remove(enemy);
        
        private bool HasTarget()
        {
            _targets.RemoveAll(x => !x.activeSelf);
            return _targets.Count > 0;
        } 
        
        private void Attack(GameObject target) 
        {
            var projectile = _poolApplication.Create(_settings.ProjectilePrefab, transform);
            projectile.transform.position += Vector3.up * _settings.HeightIndent; 
            projectile.m_target = target; // TODO plug for example: SetTarget(target)
            
            _lastShotTime = Time.time;
        }
    }
}