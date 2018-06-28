using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuerretPlacer : MonoBehaviour {

	[SerializeField]
	private GameObject turretPrefab;

	[SerializeField]
	private TowerToPlace[] towersToPlace;

	private int currentPrefabIndex = -1;

	// This is donw really bad, but for now it works
	[SerializeField]
	private GameObject gun;

	[SerializeField]
	private LayerMask placableLayers;

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
		for(int i = 0; i < towersToPlace.Length; i++) {
			if(Input.GetKeyDown(KeyCode.Alpha0 + 1 + i)) {
				if(PressedKeyOfCurrentPrefab(i)) {
					Destroy(currentTurret);
					gun.SetActive(true);
					currentPrefabIndex = -1;
				} else {
					if(currentTurret == null) {
						Destroy(currentTurret);
					} 
					currentTurret = Instantiate(towersToPlace[i].towerPrefab);
					currentPrefabIndex = i;
					gun.SetActive(false);
				}
				break;
			}
		}

		if(Input.GetMouseButtonDown(1)) {
			Destroy(currentTurret);
			gun.SetActive(true);
			currentPrefabIndex = -1;
		}
	}

    private bool PressedKeyOfCurrentPrefab(int i)
    {
        return currentPrefabIndex == i && currentTurret != null;
    }

    private void MoveCurrentTurret() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit hitInfo;

		// TODO: display to the player if he can afford the tower or not
		// TODO: Make the ray only hit terrain where it can also be placed
		if(Physics.Raycast(ray, out hitInfo, Mathf.Infinity, placableLayers)) {
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
		if(Input.GetMouseButtonDown(0) && player.Money >= towersToPlace[currentPrefabIndex].towerCost) {
			foreach(MeshCollider col in currentTurret.GetComponentsInChildren<MeshCollider>()) {
				col.enabled = true;
			}
			currentTurret.GetComponent<Tower>().Placed = true;
			currentTurret = null;
			player.Money -= towersToPlace[currentPrefabIndex].towerCost;
			gun.SetActive(true);
		}
	}

}

[System.Serializable]
public class TowerToPlace {
	public GameObject towerPrefab;
	public int towerCost;
}
