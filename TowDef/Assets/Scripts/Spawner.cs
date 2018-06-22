﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	[Header("Unity Setup")]

	[SerializeField]
	private Transform spawnPoint;
	[SerializeField]
	private Transform nexus;

	[SerializeField]
	private List<Wave> waves = new List<Wave>();

	private List<List<EnemyWithTime>> e = new List<List<EnemyWithTime>>();

	public TextAsset[] files;
	public GameObject enemyPrefab;

	private bool spawning = false;

    public bool Spawning  {
		get {
			return spawning;
		}

		private set {
			spawning = value;
		}
	}

	private void Awake() {
		LoadInWaves();
	}

	private void LoadInWaves() {
		for(int i = 0; i < files.Length; i++) {
			var list = new List<EnemyWithTime>();

			var datas = CSVReader.ReadFile(files[i]);

			foreach(var data in datas) {
				list.Add(new EnemyWithTime(enemyPrefab, int.Parse(data[1])));
			}
		}
	}

	public IEnumerator SpawnWave(int waveNumber) {
		if(waves[waveNumber] == null)
			yield break;

		Spawning = true;
		foreach(EnemyWithTime e in waves[waveNumber].EnemyWithTime) {
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
