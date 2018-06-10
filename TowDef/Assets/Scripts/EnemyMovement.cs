using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

	private NavMeshAgent agent;

	// Use this for initialization
	void Awake () {
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		if(agent.remainingDistance < 0.1f && agent.pathStatus == NavMeshPathStatus.PathComplete) {
			Destroy(gameObject);
			return;
		}
	}

	public void SetDestination(Transform destination) {
		agent.destination = destination.position;
	} 

}
