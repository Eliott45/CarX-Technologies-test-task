using System;
using System.Collections.Generic;
using System.Linq;
using GameCore.Notifiers;
using GameCore.Projectiles.Factory;
using GameCore.Settings.Keywords;
using GameCore.Settings.Repositories;
using GameCore.Settings.Towers;
using UnityEngine;
using Zenject;

namespace GameCore.Towers.Controllers
{
    public class SimpleTowerController : MonoBehaviour
    {
        [SerializeField] private ETowerKeyword _towerSettingsType = ETowerKeyword.SimpleTower;
        [SerializeField] private EnemyNotifier _enemyNotifier;
        [SerializeField] private Transform _muggleTransform;

        private readonly List<GameObject> _targets = new List<GameObject>(5);
        
        private IProjectilesFactory _projectilesFactory;
        private TowerSettingsRepository _towerSettingsRepository;

        private TowerSettings _settings;
        private float _lastShotTime = float.NegativeInfinity;

        [Inject]
        public void Construct(IProjectilesFactory projectilesFactory, TowerSettingsRepository settings)
        {
            _projectilesFactory = projectilesFactory ?? throw new NullReferenceException(nameof(IProjectilesFactory));
            _towerSettingsRepository = settings 
                ? settings 
                : throw new NullReferenceException(nameof(TowerSettings));
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
            _settings = _towerSettingsRepository.GetTowerSettings(_towerSettingsType);
        }

        private void Start()
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
            var projectile = _projectilesFactory.CreateProjectile(_settings.ProjectileKeyword, _muggleTransform);
            projectile.SetTarget(target);
            
            _lastShotTime = Time.time;
        }
    }
}