using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private Transform target;

	[SerializeField]
	private float speed = 70f;
	
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

	private void HitTarget() {
		Destroy(gameObject);
	}

	public void OnTriggerEnter(Collider collider) {
		GameObject go = collider.gameObject;
		if(go.CompareTag("Enemy")) {
			Destroy(gameObject);
			Debug.Log("HIT");
		}
	}

}
