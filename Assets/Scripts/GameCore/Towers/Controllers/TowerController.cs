using System;
using System.Collections.Generic;
using System.Linq;
using GameCore.Enemies;
using GameCore.Notifiers;
using GameCore.Projectiles.Factory;
using GameCore.Settings.Keywords;
using GameCore.Settings.Repositories;
using GameCore.Settings.Towers;
using UnityEngine;
using Zenject;

namespace GameCore.Towers.Controllers
{
    public abstract class TowerController : MonoBehaviour
    {
        [SerializeField] private ETowerKeyword _towerSettingsType = ETowerKeyword.SimpleTower;
        [SerializeField] private EnemyNotifier _enemyNotifier;
        [SerializeField] private protected Transform muggleTransform;
        
        private protected IProjectilesFactory projectilesFactory;
        private protected TowerSettings settings;

        private readonly List<IEnemy> _targets = new List<IEnemy>(5);
        
        private TowerSettingsRepository _towerSettingsRepository;
        private float _lastShotTime = float.NegativeInfinity;
        
        [Inject]
        public void Construct(IProjectilesFactory factory, TowerSettingsRepository settingsRepository)
        {
            projectilesFactory = factory ?? throw new NullReferenceException(nameof(IProjectilesFactory));
            _towerSettingsRepository = settingsRepository 
                ? settingsRepository 
                : throw new NullReferenceException(nameof(TowerSettingsRepository));
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
        
        private protected virtual void Awake()
        {
            settings = _towerSettingsRepository.GetTowerSettings(_towerSettingsType);
        }

        private protected virtual void Start()
        {
            _enemyNotifier.UpdateNotifierRadius(settings.AttackRange);
        }

        protected virtual void Attack(IEnemy target) 
        {
            _lastShotTime = Time.time;
        }
        
        protected virtual void Update () {
            
            if (!IsAvailableToAttack())
                return;
            
            Attack(_targets.First());
        }
        
        private void OnEnemyAppear(IEnemy enemy) => 
            _targets.Add(enemy);

        private void OnEnemyDisappear(IEnemy enemy) => 
            _targets.Remove(enemy);
        
        private bool IsAvailableToAttack() => 
            !(_lastShotTime + settings.AttackReloading > Time.time) && HasTarget();

        private bool HasTarget()
        {
            _targets.RemoveAll(x => !x.GetGameObject().activeSelf);
            return _targets.Count > 0;
        }
    }
}