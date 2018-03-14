using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using DSA.Extensions.Base;

namespace DSA.Extensions.Base.Editor
{
	[CustomPropertyDrawer(typeof(StoryIndex))]
	public class StoryIndexDrawer : PropertyDrawer
	{
		private SerializedProperty mythID;
		private SerializedProperty storyID;
		private SerializedProperty threadID;
		private SerializedProperty stageID;

		protected void SetProperties(SerializedProperty sentProperty)
		{
			mythID = sentProperty.FindPropertyRelative("mythID");
			storyID = sentProperty.FindPropertyRelative("storyID");
			threadID = sentProperty.FindPropertyRelative("threadID");
			stageID = sentProperty.FindPropertyRelative("stageID");
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			mythID = property.FindPropertyRelative("mythID");
			storyID = property.FindPropertyRelative("storyID");
			threadID = property.FindPropertyRelative("threadID");
			stageID = property.FindPropertyRelative("stageID");
			Rect newPosition = new Rect(position.x, position.y, position.width, 0F);
			//draw story index label
			newPosition = EditorTool.DrawLabel(newPosition, "Story Index");
			//draw myth id
			newPosition = EditorTool.DrawIntField(newPosition, mythID, "Myth ID");
			//draw story id
			newPosition = EditorTool.DrawIntField(newPosition, storyID, "Story ID");
			//draw thread id
			newPosition = EditorTool.DrawIntField(newPosition, threadID, "Thread ID");
			//draw stage id
			newPosition = EditorTool.DrawIntField(newPosition, stageID, "Stage ID");
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorTool.GetAddedHeight(EditorTool.LineHeight) * 5F;
		}
	}
}