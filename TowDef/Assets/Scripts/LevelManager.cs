using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// TODO: Take all UI Stuff and move it to an extra file
public class LevelManager : MonoBehaviour {

	[SerializeField]
	private Spawner[] spawners;

	[SerializeField]
	private int maxWaves = 1;

	private int currentWave = 0;
	
	// Some UI Elements
	[SerializeField]
	private TextMeshProUGUI waveCounter;

	[SerializeField]
	private GameObject startNextWaveHint; 

	// At the moment for debugging
	public TextMeshProUGUI phaseText;
	
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
			levelPhase = value;
		}
	}

	private void Awake() {
		waveCounter.text = "Wave " + currentWave + "/" + maxWaves;

		phaseText.color = new Color(phaseText.color.r, phaseText.color.g, phaseText.color.b, 0f);
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

		if(!SpawningEnemies && enemies.Length == 0) {
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
		startNextWaveHint.SetActive(true);

		phaseText.text = "Build";
		StartCoroutine(FadeInAndOut(1f, phaseText));
	}

	private void StartNextWavePhase() {
		foreach(Spawner spawner in spawners) {
			StartCoroutine(spawner.SpawnWave(currentWave));
		}
		currentWave++;
		waveCounter.text = "Wave " + currentWave + "/" + maxWaves;
		startNextWaveHint.SetActive(false);

		phaseText.text = "Wave";
		StartCoroutine(FadeInAndOut(1f, phaseText));
	}

	private void StartEndPhase() {
		phaseText.text = "End";
		StartCoroutine(FadeInAndOut(1f, phaseText));
	}


	private bool SpawningEnemies {
		get {
			foreach(Spawner spawner in spawners) {
				if(spawner.Spawning)
					return true;
			}
			return false;
		}
	}

	private IEnumerator FadeInAndOut(float t, TextMeshProUGUI i) {
		i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }

		yield return 3f;

		i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
	}

	private IEnumerator FadeTextToFullAlpha(float t, TextMeshProUGUI i) {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }
 
    private IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI i) {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }

}

enum LevelPhase {
	BuildPhase,
	WavePhase,
	EndPhase
}
