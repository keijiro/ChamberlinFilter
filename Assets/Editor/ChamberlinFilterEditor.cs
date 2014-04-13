using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ChamberlinFilter))]
public class ChamberlinFilterEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		var filter = target as ChamberlinFilter;
		EditorGUILayout.HelpBox("Cutoff: " + filter.CutoffFrequency + " Hz", MessageType.None);
	}
}
