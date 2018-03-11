using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DSA.Extensions.Base;

namespace DSA.Extensions.Base.Editor
{
	[CustomEditor(typeof(DataUIButton), true)]
	[CanEditMultipleObjects]
	//Editor script required to display defualt Button UI in Unity Editor
	public class UIButtonEditor : UnityEditor.UI.ButtonEditor
	{
		SerializedProperty preText;
		SerializedProperty postText;
		SerializedProperty textBox;
		SerializedProperty isSelectableAsClick;

		protected override void OnEnable()
		{
			base.OnEnable();
		}

		public override void OnInspectorGUI()
		{
			DataUIButton button = (DataUIButton)target;
			isSelectableAsClick = serializedObject.FindProperty("isSelectableAsClick");
			preText = serializedObject.FindProperty("preText");
			postText = serializedObject.FindProperty("postText");
			textBox = serializedObject.FindProperty("textBox");
			EditorGUILayout.PropertyField(isSelectableAsClick, true);
			EditorGUILayout.PropertyField(preText, true);
			EditorGUILayout.PropertyField(postText, true);
			EditorGUILayout.PropertyField(textBox, true);
			serializedObject.ApplyModifiedProperties();
			base.OnInspectorGUI();
		}
	}
}