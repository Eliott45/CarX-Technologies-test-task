using UnityEngine;

namespace GameCore.Enemies
{
    public interface IEnemy
    {
        GameObject GetEnemyGameObject();
        void SetTargetPosition(Vector3 position);
    }
}