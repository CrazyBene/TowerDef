using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	[Header("Unity Setup")]

	[SerializeField]
	private Transform spawnPoint;
	[SerializeField]
	private Transform nexus;

	[SerializeField]
	private GameObject canvas;

	private List<List<EnemyWithTime>> enemyWaves = new List<List<EnemyWithTime>>();

	public TextAsset[] files;

	public SpawnManager manager;

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

			if(files[i] != null) {

				var datas = CSVReader.ReadFile(files[i]);

				foreach(var data in datas) {
					list.Add(new EnemyWithTime(manager.stringToEnemyDic[data[0]], int.Parse(data[1])));
				}
			}
			enemyWaves.Add(list);
		}
	}

	public void PrepareForNextWave(int waveNumber) {
		if(enemyWaves[waveNumber-1].Count == 0)
			canvas.SetActive(false);
		else
			canvas.SetActive(true);
	}

	public IEnumerator SpawnWave(int waveNumber) {
		if(enemyWaves[waveNumber] == null)
			yield break;

		Spawning = true;
		foreach(EnemyWithTime e in enemyWaves[waveNumber]) {
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
