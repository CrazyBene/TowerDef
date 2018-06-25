using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float health = 10f;
	
	public void TakeDamage(float damage) {
		health -= damage;

		if(health <= 0) {
			Destroy(gameObject);
		}
	}

	public void Die() {
		Destroy(gameObject);
		return;
	}

}
