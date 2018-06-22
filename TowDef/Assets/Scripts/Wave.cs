using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Wave", menuName = "TowerDefense/Wave", order = 1)]
public class Wave : ScriptableObject {

	public List<EnemyWithTime> EnemyWithTime;

}
