using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A simple reader for a CSV File
public class CSVReader {

	private static char lineSeperator = '\n';
	private static char fieldSeperator = ';';

	/// <summary>
	/// Reads in a CSV File and returns the result
	/// </summary>
	/// <param name="csvFile">The TextAssets file which will be read.</param>
	/// <returns>Returns a List of strings inside a List for each line which contains the data of the file.</returns>
	public static List<List<string>> ReadFile(TextAsset csvFile) {
		List<List<string>> result = new List<List<string>>();

		string[] lines = csvFile.text.Split(lineSeperator);

		foreach(string line in lines) {
			List<string> currentLine = new List<string>();

			foreach(string field in line.Split(fieldSeperator)) {
				currentLine.Add(field);
			}

			result.Add(currentLine);
		}

		return result;
	}

}