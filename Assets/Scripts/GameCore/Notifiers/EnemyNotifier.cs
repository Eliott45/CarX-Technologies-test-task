using System;
using GameCore.Enemies;
using UnityEngine;

namespace GameCore.Notifiers
{
    public class EnemyNotifier : MonoBehaviour, ITargetNotifier<GameObject>
    {
        [SerializeField] private SphereCollider _sphereCollider;
        
        public event Action<GameObject> OnTargetEnter;
        public event Action<GameObject> OnTargetExit;

        public void UpdateNotifierRadius(float radius) => 
            _sphereCollider.radius = radius;

        private void OnTriggerEnter(Collider potentialTarget)
        {
            if (potentialTarget.TryGetComponent<IEnemy>(out var enemy))
                OnTargetEnter?.Invoke(enemy.GetEnemyGameObject());
        }

        private void OnTriggerExit(Collider potentialTarget)
        {
            if (potentialTarget.TryGetComponent<IEnemy>(out var enemy))  
                OnTargetExit?.Invoke(enemy.GetEnemyGameObject());
        }
    }
}
