using System;
using UnityEngine;

namespace GameCore.Notifiers
{
    public class EnemyNotifier : MonoBehaviour, ITargetNotifier
    {
        [SerializeField] private SphereCollider _sphereCollider;
        
        public event Action<GameObject> OnTargetEnter;
        public event Action<GameObject> OnTargetExit;

        public void UpdateNotifierRadius(float radius) => 
            _sphereCollider.radius = radius;

        private void OnTriggerEnter(Collider potentialTarget)
        {
            if (potentialTarget.TryGetComponent<Monster>(out var enemy)) // TODO IEnemy
                OnTargetEnter?.Invoke(enemy.gameObject);
        }

        private void OnTriggerExit(Collider potentialTarget)
        {
            if (potentialTarget.TryGetComponent<Monster>(out var enemy))  // TODO IEnemy
                OnTargetExit?.Invoke(enemy.gameObject);
        }
    }
}
