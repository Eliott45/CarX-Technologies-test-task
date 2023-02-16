using GameCore.Enemies;
using UnityEngine;

namespace GameCore.Projectiles
{
	public class GuidedProjectile : Projectile {
		public float m_speed = 0.2f;
		public int m_damage = 10;
		
		private void Update () {
			if (_target == null) {
				Destroy (gameObject);
				return;
			}

			var translation = _target.transform.position - transform.position;
			if (translation.magnitude > m_speed) {
				translation = translation.normalized * m_speed;
			}
			transform.Translate (translation);
		}

		void OnTriggerEnter(Collider other) {
			var monster = other.gameObject.GetComponent<Monster> ();
			if (monster == null)
				return;

			monster.TakeDamage(m_damage);
			Destroy (gameObject);
		}
	}
}
