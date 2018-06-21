using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVReader : MonoBehaviour {

	[SerializeField]
	private TextAsset file;

	private char lineSeperator = '\n';
	private char fieldSeperator = ';';

	private List<LineObject> objects = new List<LineObject>();


	void Start () {

		string[] lines = file.text.Split(lineSeperator);

		foreach(string line in lines) {
			string[] fields = line.Split(fieldSeperator);

			string text = fields[0];
			int number = int.Parse(fields[1]);

			objects.Add(new LineObject(text, number));
		}

		foreach(LineObject o in objects) {
			Debug.Log(o.ToString());
		}

	}

}


class LineObject {
	string text;
	int number;

	public LineObject(string text, int number) {
		this.text = text;
		this.number = number;
	}

	public override string ToString() {
		return text + " : " + number;
	}
}