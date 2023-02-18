using System;
using Extensions.PoolingSystem.Application;
using GameCore.Notifiers;
using UnityEngine;
using Zenject;

namespace GameCore.Enemies
{
    public class DestinationPoint : MonoBehaviour
    {
        [SerializeField] private EnemyNotifier _enemyNotifier;
        
        private IPoolApplication _poolApplication;

        [Inject]
        public void Construct(IPoolApplication poolApplication)
        {   
            _poolApplication = poolApplication ?? throw new NullReferenceException(nameof(IPoolApplication));
        }
        
        private void OnEnable()
        {
            _enemyNotifier.OnTargetEnter += OnEnemyArrive;
        }

        private void OnDisable()
        {
            _enemyNotifier.OnTargetEnter -= OnEnemyArrive;
        }

        private void OnEnemyArrive(IEnemy enemy) => 
            _poolApplication.Return(enemy.GetGameObject());
    }
}
