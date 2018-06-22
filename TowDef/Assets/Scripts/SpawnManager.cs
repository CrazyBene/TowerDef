using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
	
	[SerializeField]
	private Spawner[] spawners;

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
