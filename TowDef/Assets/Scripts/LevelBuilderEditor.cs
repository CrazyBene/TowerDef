using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
[CustomEditor(typeof(LevelBuilder))]
public class LevelBuilderEditor : Editor {

	#if UNITY_EDITOR
	public override void OnInspectorGUI() {
		DrawDefaultInspector();

		LevelBuilder levelBuilder = (LevelBuilder)target;
		if(GUILayout.Button("Build Level")) {
			levelBuilder.BuildLevel();
		}
	}
	#endif

}
#endif
