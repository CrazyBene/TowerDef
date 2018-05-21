using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

	[Header("Attributes")]
	[SerializeField]
	private float range = 10f;

	[SerializeField]
	private float fireRate = 1f;

	private float fireCountdown = 0f;


	[Header("Unity Setup")] 
	[SerializeField]
	private Transform firePoint;

	[SerializeField]
	private GameObject partToRotate;

	[SerializeField]
	private float turnSpeed = 10f;

	[SerializeField]
	private GameObject bulletPrefab;

	public LayerMask layerMask;



	private List<GameObject> enemiesInRange = new List<GameObject>();

	public void Awake() {
		SphereCollider rangeTrigger = gameObject.AddComponent<SphereCollider>();
		rangeTrigger.isTrigger = true;
		rangeTrigger.radius = 10;		
	}

	public void Update() {

		if(fireCountdown > 0f)
			fireCountdown -= Time.deltaTime;


		if(closestEnemy == null)
			return;

		Vector3 dir = closestEnemy.transform.position - partToRotate.transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		partToRotate.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

		// Draw a line for testing
		Debug.DrawRay(firePoint.transform.position, closestEnemy.transform.position - firePoint.transform.position);


		if(fireCountdown <= 0f) {
			Shoot();
			fireCountdown = 1f / fireRate;
		}
	}

	private void Shoot() {
		GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		Bullet bullet = bulletGO.GetComponent<Bullet>();

		if(bullet != null) {
			bullet.Seek(closestEnemy.transform);
		}
	}


	// Triggers and functions for enemy detection
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
				float dist = (enemy.transform.position - transform.position).magnitude;
				Vector3 dir = enemy.transform.position - firePoint.transform.position;
				if(dist < closestDist) {
					RaycastHit hit;
					// This part has problem with the tower model
					if(Physics.Raycast(firePoint.transform.position, dir, out hit, Mathf.Infinity, ~layerMask)) {
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
