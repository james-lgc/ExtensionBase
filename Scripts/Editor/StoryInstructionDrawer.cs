using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using DSA.Extensions.Base;

namespace DSA.Extensions.Base.Editor
{
	[CustomPropertyDrawer(typeof(StoryInstruction))]
	public class StoryInstructionDrawer : PropertyDrawer
	{
		private SerializedProperty storyIndex;
		private SerializedProperty instruction;
		private SerializedProperty id;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			storyIndex = property.FindPropertyRelative("sIndex");
			instruction = property.FindPropertyRelative("instruction");
			id = property.FindPropertyRelative("stagePointID");

			Rect newPosition = new Rect(position.x, position.y, position.width, 0F);
			newPosition = EditorTool.DrawPropertyField(newPosition, storyIndex, usePadding: false);
			newPosition = EditorTool.DrawPropertyField(newPosition, instruction, "Instruction");
			if (instruction.enumValueIndex == 4)
			{
				newPosition = EditorTool.DrawIntField(newPosition, id, "ID");
			}
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			storyIndex = property.FindPropertyRelative("sIndex");
			instruction = property.FindPropertyRelative("instruction");
			float totalHeight = EditorTool.GetHeight(storyIndex);
			totalHeight += EditorTool.lineHeight;
			if (instruction.enumValueIndex == 4)
			{
				totalHeight += EditorTool.lineHeight;
			}
			return totalHeight;
		}
	}
}