using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuerretPlacer : MonoBehaviour {

	[SerializeField]
	private GameObject turretPrefab;

	[SerializeField]
	private int turretCost;

	[SerializeField]
	private KeyCode turretHotkey = KeyCode.A;

	// Maybe move that to the player directly
	[SerializeField]
	private int playerMoney = 100;

	private GameObject currentTurret;
    private float mouseWheelRotation;

    void Update () {
		HandleNewObjectHotkey();

		if(currentTurret != null) {
			MoveCurrentTurret();
			RotateCurrentTurret();
			ReleaseIfClicked();
		}
	}

	private void HandleNewObjectHotkey() {
		if(Input.GetKeyDown(turretHotkey)) {
			if(currentTurret == null) {
				currentTurret = Instantiate(turretPrefab);
			} else {
				Destroy(currentTurret);
			}
		}
	}

	private void MoveCurrentTurret() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit hitInfo;

		// TODO: Maybe dont use the tower directly or disable the tower script, 
		// 		 AND also display to the player if he can afford the tower or not
		// TODO: Make the ray only hit terrain where it can also be placed
		if(Physics.Raycast(ray, out hitInfo)) {
			currentTurret.transform.position = hitInfo.point;
			currentTurret.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
		}
	}

	// Maybe make it only rotateable by 90 degree
	private void RotateCurrentTurret() {
		mouseWheelRotation += Input.mouseScrollDelta.y;
		currentTurret.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
	}

	private void ReleaseIfClicked() {
		if(Input.GetMouseButtonDown(0) && playerMoney >= turretCost) {
			foreach(MeshCollider col in currentTurret.GetComponentsInChildren<MeshCollider>()) {
				col.enabled = true;
			}
			currentTurret = null;

			playerMoney -= turretCost;
		}
	}

}
