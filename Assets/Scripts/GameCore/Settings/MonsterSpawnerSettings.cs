using GameCore.Enemies;
using UnityEngine;

namespace GameCore.Settings
{
    [CreateAssetMenu(fileName = nameof(MonsterSpawnerSettings), menuName = "GameSettings/" + nameof(MonsterSpawnerSettings))]
    public class MonsterSpawnerSettings : ScriptableObject
    {
        [SerializeField] private Monster _monsterPrefab;
        [SerializeField] private float _monsterSpawnInterval;

        public Monster MonsterPrefab => _monsterPrefab;
        public float MonsterSpawnInterval => _monsterSpawnInterval;
    }
}