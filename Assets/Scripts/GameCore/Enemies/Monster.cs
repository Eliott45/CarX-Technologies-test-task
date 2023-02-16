using System;
using Extensions.PoolingSystem.Application;
using UnityEngine;

namespace GameCore.Enemies
{
    public class Monster : MonoBehaviour, IEnemy
    {
        const float m_reachDistance = 0.3f; // TODO remove it pls

        private IPoolApplication _poolApplication;
        
        private int _hp;
        private float _speed;
        private Vector3 _destination;

        public void Init(IPoolApplication poolApplication)
        {
            _poolApplication = poolApplication ?? throw new NullReferenceException(nameof(IPoolApplication));
        }

        public void TakeDamage(int damage)
        {
            _hp -= damage;
            if(_hp <= 0 ) 
                Die();
        }

        public void SetCurrentPosition(Vector3 position) => 
            gameObject.transform.position = position;

        public void SetTargetPosition(Vector3 position) => 
            _destination = position;

        public void SetMaxHp(int hp) => 
            _hp = hp;

        public void SetSpeed(float speed) => 
            _speed = speed;
        
        public GameObject GetGameObject() => 
            gameObject;

        private void Die() => 
            _poolApplication.Return(gameObject);

        private void Update () { 
            /* TODO destroy on other class
            if (Vector3.Distance (transform.position, _destination) <= m_reachDistance) {
                Destroy (gameObject);
                return;
            }
            */
            
            var translation = _destination - transform.position; // todo rigidbody move
            if (translation.magnitude > _speed) {
                translation = translation.normalized * _speed;
            }
            transform.Translate (translation);
        }
    }
}