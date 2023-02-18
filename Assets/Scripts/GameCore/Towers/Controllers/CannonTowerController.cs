using System;
using GameCore.Enemies;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace GameCore.Towers.Controllers
{
    public class CannonTowerController : TowerController
    {
        [SerializeField] private Transform _cannonTransform;
        [SerializeField] private int _simulateTrajectoryStepsCount = 100;
        [SerializeField] private float _simulateTrajectoryStep = 0.1f;
        [SerializeField] private float _maxDistanceErrorRate = 0.85f;
        [SerializeField] private float _maxTimeErrorRate = 0.09f;
        [SerializeField] private float _drawDuration = 5f;
        

        private readonly float _gravity = -Physics.gravity.y;
        
        private float _launchSpeed;

        private protected override void Awake()
        {
            base.Awake();
            CalculateLaunchSpeed();
        }

        protected override void Attack(IEnemy target)
        {
            var targetVelocity = target.GetVelocity(); 
            var targetGameObject = target.GetGameObject();
            var projectileVelocity = SimulateTrajectories(targetGameObject.transform.position, targetVelocity);

            if (projectileVelocity == Vector3.zero)
                return;
            
            var projectile = projectilesFactory.CreateProjectile(settings.ProjectileKeyword, muggleTransform);
            projectile.SetTarget(targetGameObject);
            projectile.Launch(projectileVelocity, ForceMode.VelocityChange);
            
            base.Attack(target);
        }

        private void CalculateLaunchSpeed()
        {
            var x = settings.AttackRange; 
            var y = -transform.position.y;
            _launchSpeed = Mathf.Sqrt(_gravity * (y + Mathf.Sqrt(x * x + y * y)));
        }

        private Vector3 SimulateTrajectories(Vector3 targetPosition, Vector3 targetVelocity)
        {
            for (var i = 0; i < _simulateTrajectoryStepsCount; i++) 
            {
                var t = i * _simulateTrajectoryStep;
                var featurePos = targetPosition + targetVelocity * t;
                if (IsTouchPoint(featurePos, t))
                {
                    return CalculateVelocity(featurePos);
                }
            }

            return Vector3.zero;
        }
        
        private bool IsTouchPoint(Vector3 targetPosition, float time)
        {
            var dir = CalculateTriangle(targetPosition);
            var x = dir.magnitude;
            dir /= x;

            var tanTheta = CalculateTanTheta(x);
            
            if (tanTheta == 0)
                return false;

            var cosTheta = Mathf.Cos(Mathf.Atan(tanTheta));
            var sinTheta = cosTheta * tanTheta;
            
            for (var i = 1; i <= _simulateTrajectoryStepsCount; i++) {
                var t = i * _simulateTrajectoryStep;
                var dx = _launchSpeed * cosTheta * t;
                var dy = _launchSpeed * sinTheta * t - 0.5f * _gravity * t * t;
                var next = muggleTransform.position + new Vector3(dir.x * dx, dy, dir.y * dx);

                if (Vector3.Distance(next, targetPosition) <= _maxDistanceErrorRate && Math.Abs(time - t) < _maxTimeErrorRate)
                {
                    return true;
                }
            }
            
            return false;
        }
        
        private Vector3 CalculateVelocity(Vector3 targetPosition)
        {
            var launchPoint = muggleTransform.position;
            
            var dir = CalculateTriangle(targetPosition);
            var x = dir.magnitude;
            dir /= x;

            Debug.DrawLine(launchPoint, targetPosition, Color.red,_drawDuration);
            Debug.DrawLine(
                new Vector3(launchPoint.x, 0.01f, launchPoint.z),
                new Vector3(launchPoint.x + dir.x * x, 0.01f, launchPoint.z + dir.y * x),
                Color.green, _drawDuration
            );
            
            var tanTheta = CalculateTanTheta(x);
            
            if (tanTheta == 0)
                return Vector3.zero;
            
            var cosTheta = Mathf.Cos(Mathf.Atan(tanTheta));
            var sinTheta = cosTheta * tanTheta;
            
            Vector3 prev = launchPoint, next;
            for (var i = 1; i <= 35; i++) {
                var t = i * 0.1f;
                var dx = _launchSpeed * cosTheta * t;
                var dy = _launchSpeed * sinTheta * t - 0.5f * _launchSpeed * t * t;
                next = launchPoint + new Vector3(dir.x * dx, dy, dir.y * dx);
                Debug.DrawLine(prev, next, Color.magenta, _drawDuration);
                prev = next;
            }
            
            _cannonTransform.localRotation = Quaternion.LookRotation(new Vector3(dir.x, tanTheta, dir.y));
            
            return new Vector3(_launchSpeed * cosTheta * dir.x, _launchSpeed * sinTheta, _launchSpeed * cosTheta * dir.y);
        }

        private Vector2 CalculateTriangle(Vector3 targetPosition)
        {
            var launchPoint = muggleTransform.position;

            Vector2 dir;
            dir.x = targetPosition.x - launchPoint.x;
            dir.y = targetPosition.z - launchPoint.z;
            
            return dir;
        }
        
        private float CalculateTanTheta(float x)
        {
            var s = _launchSpeed;
            var s2 = s * s;
            
            var r = s2 * s2 - _gravity * (_gravity * x * x + 2f * -muggleTransform.position.y * s2);
            
            if (r <= 0f)
                return 0f;
            
            return (s2 + Mathf.Sqrt(r)) / (_gravity * x);
        }
    }
}