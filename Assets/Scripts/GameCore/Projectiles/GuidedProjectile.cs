using GameCore.Enemies;
using UnityEngine;

namespace GameCore.Projectiles
{
	public class GuidedProjectile : Projectile
	{
		[SerializeField] private Rigidbody _rigidbody;

		private void FixedUpdate()
		{
			if (target == null || !target.activeSelf) 
				ReturnToPool();
			Move();
		}

		private void Move()
		{
			var currentVelocity = _rigidbody.velocity;
            
			var movePosition = target.transform.position - transform.position;
			movePosition.Normalize();
			movePosition *= speed;

			var targetVelocity = transform.TransformDirection(movePosition);
			var finalVelocity = targetVelocity - currentVelocity;
            
			Vector3.ClampMagnitude(finalVelocity, default);
            
			_rigidbody.AddForce(finalVelocity, ForceMode.VelocityChange);
		}
		
		private void OnCollisionEnter(Collision potentialTarget)
		{
			if (!potentialTarget.gameObject.TryGetComponent<IEnemy>(out var enemy))
				return;
			
			enemy.TakeDamage(damage);
			ReturnToPool();
		}
	}
}
