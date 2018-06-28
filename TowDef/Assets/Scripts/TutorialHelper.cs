using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// This script can be a bit messy for the helping
public class TutorialHelper : MonoBehaviour {

	[SerializeField]
	private TextMeshProUGUI helpingTextField;

	[SerializeField]
	private KeyCode nextMessage;
	
	[SerializeField]
	private KeyCode preMessage;

	[SerializeField]
	private TextMeshProUGUI hideTipsText;
	private bool tipsShown = true;

	private int currentMessage = 0;
	public int CurrentMessage {
		get {
			return currentMessage;
		}
		set {
			currentMessage = value;
			string message = "";
			if(currentMessage > 0) {
				message += "(Previous 'Q') ";
			}
			message += helpMessages[currentMessage];
			if(currentMessage < helpMessages.Count - 1) {
				message +=  " (Next 'E')";
			}
			helpingTextField.text = message;
		}
	}

	private List<string> helpMessages = new List<string> {
		"Hello and Welcome to Laser Defend",
		"Your Goal is it, to defend your Nexus against incoming waves of enemies",
		"Your Nexus is the cube shape thing with the green healthbar on top",
		"Enemies will spawn at all spawners with an exclamation mark and will run towards your nexus",
		"But to prevent this you can place towers wich will destroy the enemies",
		"Press the corresponding number of the tower you see on the bottom to start placing a tower",
		"Press the left mouse button to place it or press the same number again to cancel it",
		"You can also destroy the enemies with your laser weapon",
		"If you are ready and placed all the towers you wanted, you can start spawning the wave by pressing 'G'",
		"Enjoy the Game!"
	};

	// Use this for initialization
	void Start () {
		CurrentMessage = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(nextMessage) && CurrentMessage < helpMessages.Count - 1 && tipsShown) {
			CurrentMessage++;
		}
		if(Input.GetKeyDown(preMessage) && CurrentMessage > 0 && tipsShown) {
			CurrentMessage--;
		}

		if(Input.GetKeyDown(KeyCode.H)) {
			if(tipsShown) {
				hideTipsText.text = "Show Tips [H]";
			} else {
				hideTipsText.text = "Hide Tips [H]";
			}
			tipsShown = !tipsShown;
			helpingTextField.enabled = tipsShown;
		}
	}


}
