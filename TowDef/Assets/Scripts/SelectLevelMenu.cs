using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectLevelMenu : MonoBehaviour {

	[SerializeField]
	private GameObject mainMenu;
	[SerializeField]
	private GameObject selectLevelMenu;

	[SerializeField]
	private TextMeshProUGUI selectedLevel;

	[SerializeField]
	private GameObject lockImage;

	[SerializeField]
	private GameObject loading;

	[SerializeField]
	private List<Object> scenes;

	private int maxLevels;

	private int currentLevel = 0;

	private int maxLevelPlayable;

	private void Awake() {
		maxLevels = scenes.Count;

		maxLevelPlayable = PlayerPrefs.GetInt("maxLevelPlayable", 1);
	}

	public void ChangeLevel(int change) {
		currentLevel += change;

		if(currentLevel <= 0)
			currentLevel = maxLevels;

		if(currentLevel == maxLevels + 1) {
			currentLevel = 0;
		}

		// Check if we can play that level
		if(maxLevelPlayable >= currentLevel) {
			lockImage.SetActive(false);

		} else {
			lockImage.SetActive(true);
		}

		selectedLevel.text = "Level: " + currentLevel;
	}

	public void StartLevel() {
		/* if(maxLevelPlayable >= currentLevel) { */
			SceneManager.LoadScene("Level" + currentLevel);
		/* } */
	}

	public void BackToMain() {
		selectLevelMenu.SetActive(false);
		mainMenu.SetActive(true);
	}

 	AsyncOperation asyncLoadLevel;
	IEnumerator LoadLevel (string sceneName){
		print("Start Loading"); 
		loading.SetActive(true);
		asyncLoadLevel = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
		while (!asyncLoadLevel.isDone){
			print("Loading the Scene"); 
			yield return null;
		}
		loading.SetActive(false);
	}
	
}
