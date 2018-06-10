using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	[SerializeField]
	private GameObject enemyPrefab;
	[SerializeField]
	private Transform spawnPoint;
	[SerializeField]
	private Transform nexus;


	private float timeBetweenSpawnsInSec = 5f;
	private float nextSpawn = 5f;

	private void Update() {
		nextSpawn -= Time.deltaTime;

		if(nextSpawn <= 0) {
			SpawnEnemy();
			nextSpawn = timeBetweenSpawnsInSec;
		}

	}

	private void SpawnEnemy() {
		GameObject enemyGameObject = Instantiate(enemyPrefab, spawnPoint.position, transform.rotation);
		EnemyMovement enemyMovement = enemyGameObject.GetComponent<EnemyMovement>();
		enemyMovement.SetDestination(nexus);
	}


}
