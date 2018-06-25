using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour {

	// Some UI Elements
	[SerializeField]
	private TextMeshProUGUI waveCounter;

	[SerializeField]
	private GameObject startNextWaveHint;

	// At the moment for debugging
	public TextMeshProUGUI phaseText;


	private void Awake() {
		phaseText.color = new Color(phaseText.color.r, phaseText.color.g, phaseText.color.b, 0f);
	}

	public void OnPhaseChanged(LevelPhase newPhase) {
		switch(newPhase) {
			case LevelPhase.BuildPhase:
				phaseText.text = "Build";
				startNextWaveHint.SetActive(true);
				break;
			case LevelPhase.WavePhase:
				startNextWaveHint.SetActive(false);
				phaseText.text = "Wave";
				break;
			case LevelPhase.EndPhase:
				startNextWaveHint.SetActive(false);
				phaseText.text = "End";
				break;
		}
		StartCoroutine(FadeInAndOut(1f, phaseText));
	}

	public void NextWave(int currentWave, int maxWaves) {
		waveCounter.text = "Wave " + currentWave + "/" + maxWaves;
	}

	private IEnumerator FadeInAndOut(float t, TextMeshProUGUI i) {
		i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
    while (i.color.a < 1.0f){
        i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
        yield return null;
    }

		yield return new WaitForSeconds(3f);

		i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
    while (i.color.a > 0.0f){
        i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
        yield return null;
    }
	}
}
