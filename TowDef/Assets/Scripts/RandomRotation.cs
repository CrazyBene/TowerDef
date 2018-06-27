using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour {

	public float when = 5f;
	private float left = 1f;

	private Vector3 target;
	
	// Update is called once per frame
	void Update () {
		
		left -= Time.deltaTime;
		if(left <= 0) {
			left = when;

			int x = Random.Range(-90, 90);
			int y = Random.Range(-90, 90);
			int z = Random.Range(-90, 90);

			target = transform.rotation.eulerAngles;
			target.x += x;
			target.y += y;
			target.z += z;
		}

		if(transform.rotation.eulerAngles != target) {
			Vector3 currentAngle = new Vector3(
				Mathf.Lerp(transform.rotation.eulerAngles.x, target.x, Time.deltaTime),
				Mathf.Lerp(transform.rotation.eulerAngles.y, target.y, Time.deltaTime),
				Mathf.Lerp(transform.rotation.eulerAngles.z, target.z, Time.deltaTime)
			);
			transform.eulerAngles = currentAngle;	
		}

	}

}
