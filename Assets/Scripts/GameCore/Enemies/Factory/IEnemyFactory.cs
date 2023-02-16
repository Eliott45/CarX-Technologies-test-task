using UnityEngine;

namespace GameCore.Enemies.Factory
{
    public interface IEnemyFactory
    {
        IEnemy CreateMonster(Monster monsterPrefab, Transform spawnPosition);
    }
}