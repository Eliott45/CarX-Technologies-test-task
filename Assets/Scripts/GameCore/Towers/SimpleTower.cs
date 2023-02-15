using UnityEngine;

namespace GameCore.Towers
{
    public class SimpleTower : MonoBehaviour
    {
        [SerializeField] private float _attackDelay = 0.5f;
        [SerializeField] private float _attackRange = 4f;
        [SerializeField] private GameObject _projectilePrefab;

        private float _lastShotTime = -0.5f;

        private void Update () {
            if (_projectilePrefab == null)
                return;
            
            Attack();
        }

        private void Attack()
        {
            foreach (var monster in FindObjectsOfType<Monster>()) {
                if (Vector3.Distance (transform.position, monster.transform.position) > _attackRange)
                    continue;

                if (_lastShotTime + _attackDelay > Time.time)
                    continue;

                // shot
                var projectile = Instantiate(_projectilePrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity) as GameObject;
                var projectileBeh = projectile.GetComponent<GuidedProjectile> ();
                projectileBeh.m_target = monster.gameObject;

                _lastShotTime = Time.time;
            }
        }
    }
}