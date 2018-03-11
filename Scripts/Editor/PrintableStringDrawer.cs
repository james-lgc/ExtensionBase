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
		protected override void DrawChildProperties(Rect position, SerializedProperty property)
		{
			Rect newPosition = new Rect(position.x, position.y, position.width, 0F);
			//draw unique id
			Debug.Log(uniqueID.intValue);
			newPosition = DrawUniqueID(newPosition);
			//draw name
			Debug.Log(name.stringValue);
			newPosition = DrawTextArea(newPosition, name, "Name");
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			SetProperties(property);
			float totalHeight = 0F;
			//UniqueID
			totalHeight += GetAddedHeight(lineHeight);
			//text
			totalHeight += GetAddedHeight(GetHeight(name));
			return totalHeight;
		}
	}
}