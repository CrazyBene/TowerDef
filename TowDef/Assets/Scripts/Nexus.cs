using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexus : MonoBehaviour {

	[SerializeField]
	public float health = 1000;

	public float currentHealth;

	private void Awake() {
		currentHealth = health;
	}

	public void TakeDamage(float damage) {
		currentHealth -= damage;
	}

	public bool IsDestroyed {
		get {
			return currentHealth <= 0;
		}
	}

	private void OnTriggerEnter(Collider collider) {
		GameObject go = collider.gameObject;
		if(go.CompareTag("Enemy")) {
			Enemy enemy = go.GetComponentInParent<Enemy>();

			TakeDamage(enemy.health);

			enemy.Die();
		}
	}

}
