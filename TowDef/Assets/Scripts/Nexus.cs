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

	public bool Destroyed {
		get {
			return currentHealth <= 0;
		}
	}

}
