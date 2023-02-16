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
    public abstract class TowerController : MonoBehaviour
    {
        [SerializeField] private ETowerKeyword _towerSettingsType = ETowerKeyword.SimpleTower;
        [SerializeField] private EnemyNotifier _enemyNotifier;
        [SerializeField] private protected Transform muggleTransform;
        
        private protected IProjectilesFactory projectilesFactory;
        private protected TowerSettings settings;
        
        private readonly List<GameObject> _targets = new List<GameObject>(5);
        
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
        
        private void Awake()
        {
            settings = _towerSettingsRepository.GetTowerSettings(_towerSettingsType);
        }

        private void Start()
        {
            _enemyNotifier.UpdateNotifierRadius(settings.AttackRange);
        }
        
                
        protected virtual void Attack(GameObject target) 
        {
            _lastShotTime = Time.time;
        }
        
        private void Update () {
            
            if (!IsAvailableToAttack())
                return;
            
            Attack(_targets.First());
        }
        
        private void OnEnemyAppear(GameObject enemy) => 
            _targets.Add(enemy);

        private void OnEnemyDisappear(GameObject enemy) => 
            _targets.Remove(enemy);
        
        private bool IsAvailableToAttack() => 
            !(_lastShotTime + settings.AttackReloading > Time.time) && HasTarget();

        private bool HasTarget()
        {
            _targets.RemoveAll(x => !x.activeSelf);
            return _targets.Count > 0;
        }
    }
}