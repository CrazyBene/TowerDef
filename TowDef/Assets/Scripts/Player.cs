using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

  [SerializeField]
	private float health = 100f;

  [SerializeField]
	private int money = 100;

  public int Money {
      get { return money; }
      set { money = value; }
  }

	public void TakeDamage(float damage) {
		health -= damage;

		if(health <= 0) {
      //TODO: handle death
		}
	}

}
