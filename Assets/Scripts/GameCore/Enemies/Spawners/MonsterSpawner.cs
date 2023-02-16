using System;
using GameCore.Enemies.Factory;
using GameCore.Settings;
using UnityEngine;
using Zenject;

namespace GameCore.Enemies.Spawners
{
	public class MonsterSpawner : MonoBehaviour {
		[SerializeField] private Transform _moveTargetTransform; // TODO inject

		private MonsterFactory _monsterFactory;
		private MonsterSpawnerSettings _monsterSpawnerSettings;
		
		private float _lastTimeSpawn = float.NegativeInfinity;

		[Inject]
		public void Construct(MonsterFactory monsterFactory, MonsterSpawnerSettings monsterSpawnerSettings)
		{
			_monsterFactory = monsterFactory ?? throw new NullReferenceException(nameof(MonsterFactory));
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
			_monsterFactory.Create(transform.position, _moveTargetTransform.position);
			
			_lastTimeSpawn = Time.time;
		}
	}
}
