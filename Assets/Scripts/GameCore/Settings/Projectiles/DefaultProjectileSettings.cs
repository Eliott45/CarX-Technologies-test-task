using GameCore.Projectiles;
using GameCore.Settings.Keywords;
using UnityEngine;

namespace GameCore.Settings.Projectiles
{
    [CreateAssetMenu(fileName = nameof(DefaultProjectileSettings), 
        menuName = "GameSettings/Projectiles/" + nameof(DefaultProjectileSettings))]
    public class DefaultProjectileSettings : ScriptableObject
    {
        [SerializeField] private EProjectileKeyword projectileKeyword;
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private int _damage = 10;
        [SerializeField] private float _speedMultiplier = 10;

        public EProjectileKeyword ProjectileKeyword => projectileKeyword;
        public Projectile ProjectilePrefab => _projectilePrefab;
        public int Damage => _damage;
        public float SpeedMultiplier => _speedMultiplier;
    }
}