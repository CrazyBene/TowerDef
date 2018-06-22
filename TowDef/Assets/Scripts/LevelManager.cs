using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpawnManager)), RequireComponent(typeof(UIManager))]
public class LevelManager : MonoBehaviour {

	private SpawnManager spawnManager;

	private UIManager uiManager;

	[SerializeField]
	public int maxWaves = 1;

	public int currentWave = 0;
	
	private LevelPhase levelPhase = LevelPhase.BuildPhase;
	// Automatic call the start function of each phase
	private LevelPhase Phase {
		get {
			return levelPhase;
		}
		set {
			switch(value) {
			case LevelPhase.BuildPhase: 
				StartBuildPhase();
				break;
			case LevelPhase.WavePhase: 
				StartNextWavePhase();
				break;
			case LevelPhase.EndPhase: 
				StartEndPhase();
				break;
			}
			uiManager.OnPhaseChanged(value);
			levelPhase = value;
		}
	}

	private void Awake() {
		spawnManager = GetComponent<SpawnManager>();
		uiManager = GetComponent<UIManager>();

		Phase = LevelPhase.BuildPhase;
	}


	private void Update () {		
		switch(Phase) {
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
			Phase = LevelPhase.WavePhase;
		}
	}

	private float timeBetweenSpawnsInSec = 2f;
	private float nextSpawn = 2f;
	private float spawnTime = 20f;
	private void UpdateWavePhase() {
		// Not the best way to detect no enemies, but it works ok
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

		if(!spawnManager.SpawningEnemies && enemies.Length == 0) {
			if(currentWave == maxWaves) 
				Phase = LevelPhase.EndPhase;
			else
				Phase = LevelPhase.BuildPhase;			
		}

		// If the nexus gets destroyed go to the endphase
	}

	private void UpdateEndPhase() {
		// If the player wants to end this level he can proceed to the menu?
	}

	private void StartBuildPhase() {
	}

	private void StartNextWavePhase() {
		spawnManager.SpawnWave(currentWave);
		uiManager.NextWave(currentWave, maxWaves);
		currentWave++;
	}

	private void StartEndPhase() {
	}

}

public enum LevelPhase {
	BuildPhase,
	WavePhase,
	EndPhase
}
