using GameCore.Projectiles;
using UnityEngine;

namespace GameCore.Settings.Projectiles
{
    [CreateAssetMenu(fileName = nameof(ProjectileSettings), 
        menuName = "GameSettings/Projectiles/" + nameof(ProjectileSettings))]
    public class ProjectileSettings : ScriptableObject
    {
        [SerializeField] private EProjectileKeyword projectileKeyword;
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private int _damage = 10;
        [SerializeField] private float _speed = 10;

        public EProjectileKeyword ProjectileKeyword => projectileKeyword;
        public Projectile ProjectilePrefab => _projectilePrefab;
        public int Damage => _damage;
        public float Speed => _speed;
    }
}