using UnityEngine;

namespace GameCore.Settings.Enemies
{
    [CreateAssetMenu(fileName = nameof(MonsterSettings), menuName = "GameSettings/Enemies/" + nameof(MonsterSettings))]
    public class MonsterSettings : ScriptableObject
    {
        [SerializeField] private float _speed;
        [SerializeField] private int _hp;

        public float Speed => _speed;
        public int Hp => _hp;
    }
}