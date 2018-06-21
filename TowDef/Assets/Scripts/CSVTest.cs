using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVTest : MonoBehaviour {

	public TextAsset file;

	// Use this for initialization
	void Start () {
		
		List<List<string>> data = CSVReader.ReadFile(file);

		foreach(List<string> line in data) {
			Debug.Log(line[0] + " : " + line[1]);
		}

	}

}
