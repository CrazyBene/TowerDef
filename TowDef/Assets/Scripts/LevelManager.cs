using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	[SerializeField]
	private Spawner[] spawners;

	[SerializeField]
	private TextMeshProUGUI waveCounter;

	[SerializeField]
	private int maxWaves = 1;

	private int currentWave = 0;
	
	// At the moment for debugging
	public TextMeshProUGUI phaseText;
	
	private LevelPhase levelPhase = LevelPhase.BuildPhase;

	private void Awake() {
		UpdateUI();
	}


	private void Update () {
		
		UpdateUI();
		
		switch(levelPhase) {
			case LevelPhase.BuildPhase: 
				UpdateBuildPhase();
				break;
			case LevelPhase.WavePhase: 
				UpdateWavePhase();
				break;
			case LevelPhase.EndPhase: 
				UpdateEndPhase();
				break;
		}

	}

	private void UpdateBuildPhase() {
		// If the player presses 'G' the build phase ends and the wave starts spawning
		if(Input.GetKeyDown(KeyCode.G)) {
			levelPhase = LevelPhase.WavePhase;
			currentWave++;
			foreach(Spawner spawner in spawners) {
				StartCoroutine(spawner.SpawnWave(currentWave));
			}
		}
	}

	private float timeBetweenSpawnsInSec = 2f;
	private float nextSpawn = 2f;
	private float spawnTime = 20f;
	private void UpdateWavePhase() {
		// TODO: Still need to detect if all enemies are destroyed
		if(!spawningEnemies) {
			if(currentWave == maxWaves) 
				levelPhase = LevelPhase.EndPhase;
			else
				levelPhase = LevelPhase.BuildPhase;
		}

		// If the nexus gets destroyed go to the endphase
	}

	private void UpdateEndPhase() {
		// If the player wants to end this level he can proceed to the menu?
	}

	
	private bool spawningEnemies {
		get {
			foreach(Spawner spawner in spawners) {
				if(spawner.Spawning)
					return true;
			}
			return false;
		}
	}

	private void UpdateUI() {
		waveCounter.text = "Wave " + currentWave + "/" + maxWaves;

		switch(levelPhase) {
			case LevelPhase.BuildPhase: 
				phaseText.text = "Build";
				break;
			case LevelPhase.WavePhase: 
				phaseText.text = "Wave";
				break;
			case LevelPhase.EndPhase: 
				phaseText.text = "End";
				break;
		}
	}

}

enum LevelPhase {
	BuildPhase,
	WavePhase,
	EndPhase
}
