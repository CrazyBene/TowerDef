using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private Transform target;

	[SerializeField]
	private float speed = 70f;
	[SerializeField]
	private float damage = 5f;

	[SerializeField]
	private GameObject impactParticle;
	
	public void Seek(Transform target) {
		this.target = target;
	}

	void Update () {

		if(target == null) {
			Destroy(gameObject);
			return;
		}

		Vector3 dir = target.position - transform.position;
		float distThisFrame = speed * Time.deltaTime;

		/* if(dir.magnitude <= distThisFrame) {
			HitTarget();
			return;
		} */

		transform.Translate(dir.normalized * distThisFrame, Space.World);
		
	}

	private void HitTarget(GameObject target) {
		// Destroy the bullet itself
		Destroy(gameObject);

		// Do damage to the enemy
		Enemy enemy = target.GetComponent<Enemy>();
		enemy.TakeDamage(damage);

		GameObject particle = Instantiate(impactParticle, transform.position, transform.rotation);
		Destroy(particle, 2f);
	}

	public void OnTriggerEnter(Collider collider) {
		GameObject go = collider.gameObject;
		if(go.CompareTag("Enemy")) {
			HitTarget(go.transform.parent.gameObject);
		}
	}

}
