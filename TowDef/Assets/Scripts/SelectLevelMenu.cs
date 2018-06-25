using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectLevelMenu : MonoBehaviour {

	[SerializeField]
	private GameObject mainMenu;
	[SerializeField]
	private GameObject selectLevelMenu;

	[SerializeField]
	private TextMeshProUGUI selectedLevel;

	[SerializeField]
	private int maxLevels = 5;

	private int currentLevel = 1;

	public void ChangeLevel(int change) {
		currentLevel += change;

		if(currentLevel == 0)
			currentLevel = maxLevels;

		if(currentLevel == maxLevels + 1) {
			currentLevel = 1;
		}

		selectedLevel.text = "Level: " + currentLevel;
	}

	// TODO: make it start the right level
	public void StartLevel() {
		Debug.Log("Start Level " + currentLevel);
	}

	public void BackToMain() {
		selectLevelMenu.SetActive(false);
		mainMenu.SetActive(true);
	}
	
}
