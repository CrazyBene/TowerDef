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

	[SerializeField]
	private GameObject gun;

	private Player player;

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

	private void Awake(){
		player = GetComponent<Player>();
	}

	private void HandleNewObjectHotkey() {
		if(Input.GetKeyDown(turretHotkey)) {
			if(currentTurret == null) {
				currentTurret = Instantiate(turretPrefab);
				gun.SetActive(false);
			} else {
				Destroy(currentTurret);
				gun.SetActive(true);
			}
		}
	}

	private void MoveCurrentTurret() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit hitInfo;

		// TODO: display to the player if he can afford the tower or not
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
		if(Input.GetMouseButtonDown(0) && player.Money >= turretCost) {
			foreach(MeshCollider col in currentTurret.GetComponentsInChildren<MeshCollider>()) {
				col.enabled = true;
			}
			currentTurret.GetComponent<Tower>().Placed = true;
			currentTurret = null;
			player.Money -= turretCost;
			gun.SetActive(true);
		}
	}

}
