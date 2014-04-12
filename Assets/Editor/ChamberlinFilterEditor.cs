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

		var stability = filter.Stability ? "(stable)" : "(unstable)";
		EditorGUILayout.HelpBox("Cutoff: " + filter.CutoffFrequency + " Hz " + stability, MessageType.None);
	}
}