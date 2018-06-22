using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWithTime {
	public GameObject enemy;
	public float time;

	public EnemyWithTime(GameObject enemy, float time) {
		this.enemy = enemy;
		this.time = time;
	}
}
