using System;
using Extensions.PoolingSystem.Application;
using UnityEngine;

namespace GameCore.Enemies
{
    public class Monster : MonoBehaviour, IEnemy
    {
        [SerializeField] private Rigidbody _rigidbody;

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
        
        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            var currentVelocity = _rigidbody.velocity;
            
            var movePosition = _destination - transform.position;
            movePosition.Normalize();
            movePosition *= _speed;

            var targetVelocity = transform.TransformDirection(movePosition);
            var finalVelocity = targetVelocity - currentVelocity;
            
            Vector3.ClampMagnitude(finalVelocity, default);
            
            _rigidbody.AddForce(finalVelocity, ForceMode.VelocityChange);
        }
    }
}