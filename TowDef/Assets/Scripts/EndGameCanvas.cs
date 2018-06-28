using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGameCanvas : MonoBehaviour {

	[SerializeField]
	private TextMeshProUGUI title;

	public void SetWinningStatus(bool won) {
		if(won) {
			title.text = "Victory";
		} else {
			title.text = "Defeat";
		}
	}

	public void BackToMenu() {
		SceneManager.LoadScene("MainMenu");
	}

}
