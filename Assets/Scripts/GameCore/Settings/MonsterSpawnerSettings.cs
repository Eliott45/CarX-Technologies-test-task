using UnityEngine;

namespace GameCore.Settings
{
    [CreateAssetMenu(fileName = nameof(MonsterSpawnerSettings), menuName = "GameSettings/" + nameof(MonsterSpawnerSettings))]
    public class MonsterSpawnerSettings : ScriptableObject
    {
        [SerializeField] private float _monsterSpawnInterval;

        public float MonsterSpawnInterval => _monsterSpawnInterval;
    }
}