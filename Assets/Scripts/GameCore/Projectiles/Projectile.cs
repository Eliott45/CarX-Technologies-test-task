using UnityEngine;

namespace GameCore.Projectiles
{
    public abstract class Projectile : MonoBehaviour, IProjectile
    {
        protected GameObject _target;
        
        public void SetTarget(GameObject target) => 
            _target = target;
    }
}