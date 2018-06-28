using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour {

	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	private void Update () {
		Vector3 dir = player.transform.position - transform.position;
		dir.y = 0;
		this.transform.rotation = Quaternion.LookRotation(dir);
	}

}
