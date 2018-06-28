using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
	
	[SerializeField]
	private Spawner[] spawners;
	public StringToPrefab[] stringToEnemy;
	public Dictionary<string, GameObject> stringToEnemyDic = new Dictionary<string, GameObject>();

	private void Awake() {
		foreach(Spawner spawner in spawners) {
			spawner.manager = this;
		}

		foreach(StringToPrefab e in stringToEnemy) {
			stringToEnemyDic.Add(e.name, e.prefab);
		}
	}

	public void PrepareNextWave(int waveNumber) {
		foreach(Spawner spawner in spawners) {
			spawner.PrepareForNextWave(waveNumber);
		}
	}

	public void SpawnWave(int waveNumber) {
		foreach(Spawner spawner in spawners) {
			StartCoroutine(spawner.SpawnWave(waveNumber));
		}
	}

	public bool SpawningEnemies {
		get {
			foreach(Spawner spawner in spawners) {
				if(spawner.Spawning)
					return true;
			}
			return false;
		}
	}

}


[System.Serializable]
public struct StringToPrefab {
	public string name;
	public GameObject prefab;
}