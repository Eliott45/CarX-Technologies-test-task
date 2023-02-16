using System;
using GameCore.Enemies.Factory;
using GameCore.Settings;
using UnityEngine;
using Zenject;

namespace GameCore.Enemies.Spawners
{
	public class MonsterSpawner : MonoBehaviour {
		[SerializeField] private Transform _moveTargetTransform; // TODO inject

		private IEnemyFactory _enemyFactory;
		private MonsterSpawnerSettings _monsterSpawnerSettings;
		
		private float _lastTimeSpawn = float.NegativeInfinity;

		[Inject]
		public void Construct(IEnemyFactory factory, MonsterSpawnerSettings monsterSpawnerSettings)
		{
			_enemyFactory = factory ?? throw new NullReferenceException(nameof(IEnemyFactory));
			_monsterSpawnerSettings = monsterSpawnerSettings 
				? monsterSpawnerSettings 
				: throw new NullReferenceException(nameof(MonsterSpawnerSettings));
		}

		private void Update () {
			if (IsAvailableToSpawn()) 
				Spawn();
		}

		private bool IsAvailableToSpawn() => 
			Time.time > _lastTimeSpawn + _monsterSpawnerSettings.MonsterSpawnInterval;

		private void Spawn()
		{
			var enemy = _enemyFactory.CreateMonster(_monsterSpawnerSettings.MonsterPrefab, transform);
			
			enemy.SetTargetPosition(_moveTargetTransform.position);
			
			_lastTimeSpawn = Time.time;
		}
	}
}
