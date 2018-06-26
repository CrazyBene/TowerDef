using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	public GameObject mainMenu;
	public GameObject selectLevelMenu;

	public void PlayGame() {
		mainMenu.SetActive(false);
		selectLevelMenu.SetActive(true);
	}

	public void QuitGame() {
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit ();
		#endif
	}

}
