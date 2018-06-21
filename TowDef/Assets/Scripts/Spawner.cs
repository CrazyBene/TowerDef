using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	[Header("Unity Setup")]

	[SerializeField]
	private Transform spawnPoint;
	[SerializeField]
	private Transform nexus;

	
	// the list for one wave, maybe change this to an external file?
	public List<EnemyWithTime> enemyToSpawn = new List<EnemyWithTime>();

	private bool spawning = false;

    public bool Spawning  {
		get {
			return spawning;
		}

		private set {
			spawning = value;
		}
	}


    private float timeBetweenSpawnsInSec = 5f;
	private float nextSpawn = 5f;

    private void Update() {

	}

	public IEnumerator SpawnWave(int waveNumber) {
		Spawning = true;
		foreach(EnemyWithTime e in enemyToSpawn) {
			yield return new WaitForSeconds(e.time);
			SpawnEnemy(e.enemy);
		}

		Spawning = false;
		yield return new WaitForEndOfFrame();
	}

	private void SpawnEnemy(GameObject thisEnemyPrefab) {
		GameObject enemyGameObject = Instantiate(thisEnemyPrefab, spawnPoint.position, transform.rotation);
		EnemyMovement enemyMovement = enemyGameObject.GetComponent<EnemyMovement>();
		enemyMovement.SetDestination(nexus);
	}


}



[System.Serializable]
public class EnemyWithTime {
	public GameObject enemy;
	public float time;
}
