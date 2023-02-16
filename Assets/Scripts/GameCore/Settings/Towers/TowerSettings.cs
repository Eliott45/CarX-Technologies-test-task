using GameCore.Settings.Keywords;
using UnityEngine;

namespace GameCore.Settings.Towers
{
    [CreateAssetMenu(fileName = nameof(TowerSettings), menuName = "GameSettings/Towers/" + nameof(TowerSettings))]
    public class TowerSettings : ScriptableObject
    {
        [SerializeField] private float _attackReloading = 10;
        [SerializeField] private float _attackRange = 10;
        [SerializeField] private ETowerKeyword _towerKeyword;
        [SerializeField] private EProjectileKeyword _projectileKeyword;

        public float AttackReloading => _attackReloading;
        public float AttackRange => _attackRange;
        public ETowerKeyword TowerKeyword => _towerKeyword;
        public EProjectileKeyword ProjectileKeyword => _projectileKeyword;
    }
}
