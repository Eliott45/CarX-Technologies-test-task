using UnityEngine;

namespace GameCore.Projectiles
{
    public abstract class Projectile : MonoBehaviour, IProjectile
    {
        protected GameObject target;
        protected float speed;
        protected int damage;
        
        public void SetTarget(GameObject targetGameObject) => 
            target = targetGameObject;

        public void SetDamage(int newDamage) => 
            damage = newDamage;

        public void SetSpeed(float newSpeed) => 
            speed = newSpeed;
    }
}