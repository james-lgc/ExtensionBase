using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DSA.Extensions.Base;

namespace DSA.Extensions.Base.Editor
{
	[CustomEditor(typeof(UIText))]
	[CanEditMultipleObjects]
	//Overrides TextEditor to display pre and post texts
	public class UITextEditor : UnityEditor.UI.TextEditor
	{
		SerializedProperty preText;
		SerializedProperty postText;

		protected override void OnEnable()
		{
			base.OnEnable();
		}

		public override void OnInspectorGUI()
		{
			UIText text = (UIText)target;
			preText = serializedObject.FindProperty("preText");
			postText = serializedObject.FindProperty("postText");
			EditorGUILayout.PropertyField(preText, true);
			EditorGUILayout.PropertyField(postText, true);
			serializedObject.ApplyModifiedProperties();
			base.OnInspectorGUI();
		}
	}
}