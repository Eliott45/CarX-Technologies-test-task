using UnityEngine;

namespace GameCore.Projectiles
{
	public class GuidedProjectile : Projectile
	{
		private protected override void FixedUpdate()
		{
			base.FixedUpdate();
			Move();
		}

		private void Move()
		{
			var currentVelocity = rb.velocity;
            
			var movePosition = target.transform.position - transform.position;
			movePosition.Normalize();
			movePosition *= speed;

			var targetVelocity = transform.TransformDirection(movePosition);
			var finalVelocity = targetVelocity - currentVelocity;
            
			Vector3.ClampMagnitude(finalVelocity, default);
            
			rb.AddForce(finalVelocity, ForceMode.VelocityChange);
		}
	}
}
