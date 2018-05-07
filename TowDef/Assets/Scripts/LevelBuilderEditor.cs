using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelBuilder))]
public class LevelBuilderEditor : Editor {

	public override void OnInspectorGUI() {
		DrawDefaultInspector();

		LevelBuilder levelBuilder = (LevelBuilder)target;
		if(GUILayout.Button("Build Level")) {
			levelBuilder.BuildLevel();
		}
	}

}
