using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuerretPlacer : MonoBehaviour {

	[SerializeField]
	private GameObject turretPrefab;

	[SerializeField]
	private KeyCode turretHotkey = KeyCode.A;

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

		if(Physics.Raycast(ray, out hitInfo)) {
			currentTurret.transform.position = hitInfo.point;
			currentTurret.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
		}
	}

	private void RotateCurrentTurret() {
		mouseWheelRotation += Input.mouseScrollDelta.y;
		currentTurret.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
	}

	private void ReleaseIfClicked() {
		if(Input.GetMouseButtonDown(0)) {
			currentTurret.GetComponent<BoxCollider>().enabled = true;
			currentTurret = null;
		}
	}

}
