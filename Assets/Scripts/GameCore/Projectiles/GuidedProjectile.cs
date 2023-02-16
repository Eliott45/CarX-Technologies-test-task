using System;
using GameCore.Enemies;
using UnityEngine;

namespace GameCore.Projectiles
{
	public class GuidedProjectile : Projectile
	{
		private void Update () {
			if (target == null) {
				Destroy (gameObject);
				return;
			}

			var translation = target.transform.position - transform.position;
			if (translation.magnitude > speed) {
				translation = translation.normalized * speed;
			}
			transform.Translate (translation);
		}
		
		private void OnCollisionEnter(Collision potentialTarget)
		{
			if (!potentialTarget.gameObject.TryGetComponent<IEnemy>(out var enemy))
				return;
			
			enemy.TakeDamage(damage);
			Destroy(gameObject);
		}
	}
}
