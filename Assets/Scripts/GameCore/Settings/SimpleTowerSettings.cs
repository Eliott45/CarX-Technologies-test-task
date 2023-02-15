using UnityEngine;

namespace GameCore.Settings
{
    [CreateAssetMenu(fileName = nameof(SimpleTowerSettings), menuName = "GameSettings/" + nameof(SimpleTowerSettings))]
    public class SimpleTowerSettings : ScriptableObject
    {
        [SerializeField] private float _attackReloading;
        [SerializeField] private float _attackRange;
        [SerializeField] private float _heightIndent;
        [SerializeField] private GuidedProjectile _projectilePrefab;

        public float AttackReloading => _attackReloading;
        public float AttackRange => _attackRange;
        public float HeightIndent => _heightIndent;
        public GuidedProjectile ProjectilePrefab => _projectilePrefab;
    }
}
