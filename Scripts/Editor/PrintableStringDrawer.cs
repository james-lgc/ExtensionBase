using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using DSA.Extensions.Base;

namespace DSA.Extensions.Base.Editor
{
	[CustomPropertyDrawer(typeof(PrintableString))]
	public class PrintableStringDrawer : DataItemDrawer
	{
		protected override Action<SerializedProperty> editAction { get; }

		protected override void DrawChildProperties(Rect position, SerializedProperty property)
		{
			Rect newPosition = new Rect(position.x, position.y, position.width, 0F);
			//draw unique id
			Debug.Log(uniqueID.intValue);
			newPosition = DrawUniqueID(newPosition);
			//draw name
			Debug.Log(name.stringValue);
			newPosition = EditorTool.DrawTextArea(newPosition, name, "Name");
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			SetProperties(property);
			float totalHeight = 0F;
			//UniqueID
			totalHeight += EditorTool.GetAddedHeight(EditorTool.LineHeight);
			//text
			totalHeight += EditorTool.GetAddedHeight(EditorTool.GetHeight(name));
			return totalHeight;
		}
	}
}