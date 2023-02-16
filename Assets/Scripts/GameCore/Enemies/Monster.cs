using UnityEngine;
using Zenject;

namespace GameCore.Enemies
{
    public class Monster : MonoBehaviour, IEnemy
    {
        public float m_speed = 0.1f;
        public int m_maxHP = 30;
        const float m_reachDistance = 0.3f;

        public int m_hp;
        
        private Vector3 _destination;
        
        [Inject]
        public void Construct(Vector3 spawnPosition, Vector3 destination)
        {
            transform.position = spawnPosition;
            _destination = destination;
        }
        
        public GameObject GetEnemyGameObject() => 
            gameObject;

        void Start() {
            m_hp = m_maxHP;
        }
        
        void Update () {
            if (Vector3.Distance (transform.position, _destination) <= m_reachDistance) {
                Destroy (gameObject);
                return;
            }

            var translation = _destination - transform.position;
            if (translation.magnitude > m_speed) {
                translation = translation.normalized * m_speed;
            }
            transform.Translate (translation);
        }
    }
}