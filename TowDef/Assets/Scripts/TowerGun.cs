using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGun : MonoBehaviour {

	[SerializeField]
	private float range = 10;

	[SerializeField]
	private Transform gunOrigin;

	private List<GameObject> enemiesInRange = new List<GameObject>();

	public void Awake() {
		SphereCollider rangeTrigger = gameObject.AddComponent<SphereCollider>();
		rangeTrigger.isTrigger = true;
		rangeTrigger.radius = 10;		
	}

	public void Update() {

		// Draw a line for testing
		if(closestEnemy != null)
			Debug.DrawRay(gunOrigin.transform.position, closestEnemy.transform.position - gunOrigin.transform.position);
	}

	public void OnTriggerEnter(Collider collider) {
		GameObject go = collider.gameObject;
		if(go.CompareTag("Enemy")) {
			AddEnemy(go);
		}
	}

	public void OnTriggerExit(Collider collider) {
		GameObject go = collider.gameObject;
		if(go.CompareTag("Enemy")) {
			RemoveEnemy(go);
		}
	}

	private void AddEnemy(GameObject enemy) {
		if(!enemiesInRange.Contains(enemy))
			enemiesInRange.Add(enemy);
	}

	private void RemoveEnemy(GameObject enemy) {
		if(enemiesInRange.Contains(enemy))
			enemiesInRange.Remove(enemy);
	}

	GameObject closestEnemy {
		get {
			float closestDist = float.MaxValue;
			GameObject closestEnemy = null;

			foreach(GameObject enemy in enemiesInRange) {
				Vector3 dir = enemy.transform.position - transform.position;
				float dist = dir.magnitude;
				if(dist < closestDist) {
					RaycastHit hit;
					// This part has problem with the tower model
					if(Physics.Raycast(gunOrigin.transform.position, dir, out hit, Mathf.Infinity)) {
						if(hit.collider.gameObject == enemy) {
							closestEnemy = enemy;
							closestDist = dist;
						}
					}
				}
			}

			return closestEnemy;
		}
	}


	// Draw method for range indicator
	public void OnDrawGizmosSelected() {
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, range);
	}

}
