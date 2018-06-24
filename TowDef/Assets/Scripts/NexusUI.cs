using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Nexus))]
public class NexusUI : MonoBehaviour {

	private Nexus nexus;

	private Transform player;

	[SerializeField]
	private Image healthbarRotation;

	[SerializeField]
	private Image healthbar;
	
	private void Awake() {
		nexus = GetComponent<Nexus>();

		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	private void Update () {
		var procent = nexus.currentHealth / nexus.health;
		healthbar.fillAmount = procent;

		Vector3 dir = player.position - transform.position;
		dir.y = 0;
		healthbarRotation.transform.rotation = Quaternion.LookRotation(dir);
	}

}
