using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSA.Extensions.Base;
using UnityEditor;
using System;

namespace DSA.Extensions.Base.Editor
{
	[CustomPropertyDrawer(typeof(InstructionData))]
	public class InstructionDataDrawer : PropertyDrawer
	{
		private SerializedProperty extension;
		private SerializedProperty name;
		private SerializedProperty identifier;
		private SerializedProperty transformValue;

		private void SetProperties(SerializedProperty sentProperty)
		{
			extension = sentProperty.FindPropertyRelative("extension");
			name = sentProperty.FindPropertyRelative("name");
			identifier = sentProperty.FindPropertyRelative("identifier");
			transformValue = sentProperty.FindPropertyRelative("transformValue");
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			SetProperties(property);
			Rect newPosition = EditorTool.DrawTopLabel(position, "Instruction Data");
			newPosition = EditorTool.DrawPropertyField(newPosition, extension, "Extension");
			string enumValue = extension.enumNames[extension.enumValueIndex];
			switch (enumValue)
			{
				case "Story":
					newPosition = EditorTool.DrawArray(newPosition, identifier, "Identifier");
					break;
			}
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			SetProperties(property);
			float totalHeight = EditorTool.InitialVerticalPadding;
			//label
			totalHeight += EditorTool.AddedLineHeight;
			//extension
			totalHeight += EditorTool.AddedLineHeight;
			string enumValue = extension.enumNames[extension.enumValueIndex];
			switch (enumValue)
			{
				case "Story":
					float identifierHeight = EditorTool.AddedLineHeight;
					for (int i = 0; i < identifier.arraySize; i++)
					{
						identifierHeight += EditorTool.GetAddedHeight(EditorTool.GetHeight(identifier.GetArrayElementAtIndex(i)));
					}
					totalHeight += identifierHeight;
					break;
			}
			return totalHeight;
		}
	}
}