using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
public class LevelBuilder : MonoBehaviour {

	[SerializeField] private Texture2D levelMap;

	[SerializeField] private Color32 groundColor;
	[SerializeField] private GameObject groundPrefab;

	[SerializeField] private Color32 wallColor;
	[SerializeField] private WallColorToPrefab wallPrefabs;

	[SerializeField] private Color32 highGroundColor;
	[SerializeField] private HighGroundColorToPrefab highGroundPrefabs;
	
	[SerializeField] private Color32 rampColor;

	public void BuildLevel() {
		
		if(levelMap == null) {
			Debug.Log("No Level to build!");
			return;
		}	

		ClearLevel();


		Color32[] allPixels = levelMap.GetPixels32();

		int width = levelMap.width;
		int height = levelMap.height;

		for(int x = 0; x < width; x++) {
			for(int y = 0; y < height; y++) {
				SpawnObjectAt(allPixels[x + y * width], x, y);
			}
		}
	}

	private void ClearLevel() {
		while(transform.childCount > 0) {
			Transform child = transform.GetChild(0);
			child.SetParent(null);
			Object.DestroyImmediate(child.gameObject);
		}
	}

	private void SpawnObjectAt(Color32 c, int x, int y) {
		if(c.Equals(groundColor) || c.Equals(rampColor)) {
			GameObject go = PrefabUtility.InstantiatePrefab(groundPrefab) as GameObject;
			go.transform.position = new Vector3(x * 2, 0, y * 2);
			go.transform.SetParent(transform);
		} else if(c.Equals(wallColor) || c.Equals(highGroundColor)) {
			int type = 0;
			// Chose the right wall prefab
			Color32[] allPixels = levelMap.GetPixels32();

			int width = levelMap.width;
			int height = levelMap.height;

			// Check Tile North
			if(y < height - 1) {
				if(wallColor.Equals(allPixels[x + (y + 1) * width]) || highGroundColor.Equals(allPixels[x + (y + 1) * width])) {
					type += 1;
				}
			} 

			// Check Tile East
			if(x < width - 1) {
				if(wallColor.Equals(allPixels[x + 1 + y * width]) || highGroundColor.Equals(allPixels[x + 1 + y * width])) {
					type += 2;
				}
			}

			// Check Tile South
			if(y > 0) {
				if(wallColor.Equals(allPixels[x + (y - 1) * width]) || highGroundColor.Equals(allPixels[x + (y - 1) * width])) {
					type += 4;
				}
			}

			// Check Tile North
			if(x > 0) {
				if(wallColor.Equals(allPixels[x - 1 + y * width]) || highGroundColor.Equals(allPixels[x - 1 + y * width])) {
					type += 8;
				}
			}

			GameObject wallPrefab = null;
			Vector3 rotation = new Vector3();

			switch(type) {
				case 0: wallPrefab = wallPrefabs.wall4Prefab; break;
				case 1: wallPrefab = wallPrefabs.wall3Prefab; rotation.y = 90; break;
				case 2: wallPrefab = wallPrefabs.wall3Prefab; rotation.y = 180; break;
				case 3: wallPrefab = wallPrefabs.wall2EPrefab; rotation.y = 180; break;
				case 4: wallPrefab = wallPrefabs.wall3Prefab; rotation.y = 270; break;
				case 5: wallPrefab = wallPrefabs.wall2OPrefab; rotation.y = 90; break;
				case 6: wallPrefab = wallPrefabs.wall2EPrefab; rotation.y = 270; break;
				case 7: wallPrefab = wallPrefabs.wall1Prefab; rotation.y = 270; break;
				case 8: wallPrefab = wallPrefabs.wall3Prefab; break;
				case 9: wallPrefab = wallPrefabs.wall2EPrefab; rotation.y = 90; break;
				case 10: wallPrefab = wallPrefabs.wall2OPrefab; break;
				case 11: wallPrefab = wallPrefabs.wall1Prefab; rotation.y = 180; break;
				case 12: wallPrefab = wallPrefabs.wall2EPrefab; break;
				case 13: wallPrefab = wallPrefabs.wall1Prefab; rotation.y = 90; break;
				case 14: wallPrefab = wallPrefabs.wall1Prefab; break;
				case 15: wallPrefab = wallPrefabs.wall0Prefab; break;
			}

			if(wallPrefab == null)
				return;

			GameObject go = PrefabUtility.InstantiatePrefab(wallPrefab) as GameObject;
			go.transform.position = new Vector3(x * 2, 0, y * 2);
			go.transform.SetParent(transform);
			go.transform.Rotate(rotation);

			// Now we just need to Place the edges
			if((type == 3 || type == 7 || type == 11 || type == 15) && (!wallColor.Equals(allPixels[x + 1 + (y + 1) * width]) && !highGroundColor.Equals(allPixels[x + 1 + (y + 1) * width]))) {
				GameObject edge = PrefabUtility.InstantiatePrefab(wallPrefabs.edgePrefab) as GameObject;
				edge.transform.position = new Vector3(x * 2, 0, y * 2);
				edge.transform.SetParent(transform);
				edge.transform.Rotate(0f, 90f, 0f);
			}
			if((type == 6 || type == 7 || type == 14 || type == 15) && (!wallColor.Equals(allPixels[x + 1 + (y - 1) * width]) && !highGroundColor.Equals(allPixels[x + 1 + (y - 1) * width]))) {
				GameObject edge = PrefabUtility.InstantiatePrefab(wallPrefabs.edgePrefab) as GameObject;
				edge.transform.position = new Vector3(x * 2, 0, y * 2);
				edge.transform.SetParent(transform);
				edge.transform.Rotate(0f, 180f, 0f);
			}
			if((type == 12 || type == 13 || type == 14 || type == 15) && (!wallColor.Equals(allPixels[x - 1 + (y - 1) * width]) && !highGroundColor.Equals(allPixels[x - 1 + (y - 1) * width]))) {
				GameObject edge = PrefabUtility.InstantiatePrefab(wallPrefabs.edgePrefab) as GameObject;
				edge.transform.position = new Vector3(x * 2, 0, y * 2);
				edge.transform.SetParent(transform);
				edge.transform.Rotate(0f, -90f, 0f);
			}
			if((type == 9 || type == 11 || type == 13 || type == 15) && (!wallColor.Equals(allPixels[x - 1 + (y + 1) * width]) && !highGroundColor.Equals(allPixels[x - 1 + (y + 1) * width]))) {
				GameObject edge = PrefabUtility.InstantiatePrefab(wallPrefabs.edgePrefab) as GameObject;
				edge.transform.position = new Vector3(x * 2, 0, y * 2);
				edge.transform.SetParent(transform);
				edge.transform.Rotate(0f, 0f, 0f);
			}

		} 
		
		if (c.Equals(highGroundColor)) {
			int type = 0;
			// Chose the right wall prefab
			Color32[] allPixels = levelMap.GetPixels32();

			int width = levelMap.width;
			int height = levelMap.height;

			// Check Tile North
			if(y > 0) {
				if(c.Equals(allPixels[x + (y - 1) * width])) {
					type += 4;
				}
			} 

			// Check Tile East
			if(x < width - 1) {
				if(c.Equals(allPixels[x + 1 + y * width])) {
					type += 2;
				}
			}

			// Check Tile South
			if(y < height - 1) {
				if(c.Equals(allPixels[x + (y + 1) * width])) {
					type += 1;
				}
			}

			// Check Tile North
			if(x > 0) {
				if(c.Equals(allPixels[x - 1 + y * width])) {
					type += 8;
				}
			}

			GameObject highGroundPrefab = null;
			Vector3 rotation = new Vector3();

			switch(type) {
				case 0: highGroundPrefab = highGroundPrefabs.high4Prefab; break;
				case 1: highGroundPrefab = highGroundPrefabs.high3Prefab; rotation.y = 90; break;
				case 2: highGroundPrefab = highGroundPrefabs.high3Prefab; rotation.y = 180; break;
				case 3: highGroundPrefab = highGroundPrefabs.high2EPrefab; rotation.y = 180; break;
				case 4: highGroundPrefab = highGroundPrefabs.high3Prefab; rotation.y = 270; break;
				case 5: highGroundPrefab = highGroundPrefabs.high2OPrefab; rotation.y = 90; break;
				case 6: highGroundPrefab = highGroundPrefabs.high2EPrefab; rotation.y = 270; break;
				case 7: highGroundPrefab = highGroundPrefabs.high1Prefab; rotation.y = 270; break;
				case 8: highGroundPrefab = highGroundPrefabs.high3Prefab; break;
				case 9: highGroundPrefab = highGroundPrefabs.high2EPrefab; rotation.y = 90; break;
				case 10: highGroundPrefab = highGroundPrefabs.high2OPrefab; break;
				case 11: highGroundPrefab = highGroundPrefabs.high1Prefab; rotation.y = 180; break;
				case 12: highGroundPrefab = highGroundPrefabs.high2EPrefab; break;
				case 13: highGroundPrefab = highGroundPrefabs.high1Prefab; rotation.y = 90; break;
				case 14: highGroundPrefab = highGroundPrefabs.high1Prefab; break;
				case 15: highGroundPrefab = highGroundPrefabs.high0Prefab; break;
			}

			if(highGroundPrefab == null)
				return;

			GameObject go = PrefabUtility.InstantiatePrefab(highGroundPrefab) as GameObject;
			go.transform.position = new Vector3(x * 2, 2.5f, y * 2);
			go.transform.SetParent(transform);
			go.transform.Rotate(rotation);

			// Now we just need to Place the edges
			if((type == 3 || type == 7 || type == 11 || type == 15) && !highGroundColor.Equals(allPixels[x + 1 + (y + 1) * width])) {
				GameObject edge = PrefabUtility.InstantiatePrefab(highGroundPrefabs.highGroundEdgePrefab) as GameObject;
				edge.transform.SetParent(transform);
				edge.transform.position = new Vector3(x * 2, 2.5f, y * 2);
				edge.transform.Rotate(0f, 90f, 0f);
			}
			if((type == 6 || type == 7 || type == 14 || type == 15) && !highGroundColor.Equals(allPixels[x + 1 + (y - 1) * width])) {
				GameObject edge = PrefabUtility.InstantiatePrefab(highGroundPrefabs.highGroundEdgePrefab) as GameObject;
				edge.transform.SetParent(transform);
				edge.transform.position = new Vector3(x * 2, 2.5f, y * 2);
				edge.transform.Rotate(0f, 180f, 0f);
			}
			if((type == 12 || type == 13 || type == 14 || type == 15) && !highGroundColor.Equals(allPixels[x - 1 + (y - 1) * width])) {
				GameObject edge = PrefabUtility.InstantiatePrefab(highGroundPrefabs.highGroundEdgePrefab) as GameObject;
				edge.transform.SetParent(transform);
				edge.transform.position = new Vector3(x * 2, 2.5f, y * 2);
				edge.transform.Rotate(0f, -90f, 0f);
			}
			if((type == 9 || type == 11 || type == 13 || type == 15) && !highGroundColor.Equals(allPixels[x - 1 + (y + 1) * width])) {
				GameObject edge = PrefabUtility.InstantiatePrefab(highGroundPrefabs.highGroundEdgePrefab) as GameObject;
				edge.transform.SetParent(transform);
				edge.transform.position = new Vector3(x * 2, 2.5f, y * 2);
				edge.transform.Rotate(0f, 0f, 0f);
			}
		}

	}

	[System.Serializable]
	private class WallColorToPrefab {
		public GameObject wall4Prefab;
		public GameObject wall3Prefab;
		public GameObject wall2EPrefab;
		public GameObject wall2OPrefab;
		public GameObject wall1Prefab;
		public GameObject wall0Prefab;		
		public GameObject edgePrefab;
	}

	// Second Class not neccassary, maybe remove later
	[System.Serializable]
	private class HighGroundColorToPrefab {
		public GameObject high4Prefab;
		public GameObject high3Prefab;
		public GameObject high2OPrefab;
		public GameObject high2EPrefab;
		public GameObject high1Prefab;
		public GameObject high0Prefab;
		public GameObject highGroundEdgePrefab;
	}

}
#endif
