using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ChamberlinFilter))]
public class ChamberlinFilterEditor : Editor
{
	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();

		var f = target as ChamberlinFilter;

		EditorGUILayout.HelpBox (
			"Cutoff: " + (Mathf.Pow (2.0f, f.cutoff * 10 - 10) * 0.25f * f.sampleRate) + "\n" +
			"Sample rate: " + f.sampleRate,
			MessageType.None
		);
	}
}
