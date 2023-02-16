using GameCore.Towers.Projectiles;
using UnityEngine;

namespace GameCore.Settings.Towers
{
    [CreateAssetMenu(fileName = nameof(SimpleTowerSettings), menuName = "GameSettings/Towers/" + nameof(SimpleTowerSettings))]
    public class SimpleTowerSettings : ScriptableObject
    {
        [SerializeField] private float _attackReloading;
        [SerializeField] private float _attackRange;
        [SerializeField] private GuidedProjectile _projectilePrefab;

        public float AttackReloading => _attackReloading;
        public float AttackRange => _attackRange;
        public GuidedProjectile ProjectilePrefab => _projectilePrefab;
    }
}
