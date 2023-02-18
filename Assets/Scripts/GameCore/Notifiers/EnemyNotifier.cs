using System;
using GameCore.Enemies;
using UnityEngine;

namespace GameCore.Notifiers
{
    public class EnemyNotifier : MonoBehaviour, ITargetNotifier<IEnemy>
    {
        [SerializeField] private SphereCollider _sphereCollider;
        
        public event Action<IEnemy> OnTargetEnter;
        public event Action<IEnemy> OnTargetExit;

        public void UpdateNotifierRadius(float radius) => 
            _sphereCollider.radius = radius;

        private void OnTriggerEnter(Collider potentialTarget)
        {
            if (potentialTarget.TryGetComponent<IEnemy>(out var enemy))
                OnTargetEnter?.Invoke(enemy);
        }

        private void OnTriggerExit(Collider potentialTarget)
        {
            if (potentialTarget.TryGetComponent<IEnemy>(out var enemy))  
                OnTargetExit?.Invoke(enemy);
        }
    }
}
