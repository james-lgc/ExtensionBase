using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DSA.Extensions.Base;

namespace DSA.Extensions.Base.Editor
{
	public abstract class DataItemDrawer : BasePropertyDrawer
	{
		protected SerializedProperty name;
		protected SerializedProperty id;
		protected SerializedProperty uniqueID;

		protected override void SetProperties(SerializedProperty sentProperty)
		{
			if (GetIsCurrentProperty(sentProperty)) { return; }
			try { name = sentProperty.FindPropertyRelative("name"); }
			catch (System.Exception e) { Debug.Log("Name property not found.\n" + e.ToString()); }
			try { id = sentProperty.FindPropertyRelative("id"); }
			catch (System.Exception e) { Debug.Log("ID property not found.\n" + e.ToString()); }
			try { uniqueID = sentProperty.FindPropertyRelative("uniqueID"); }
			catch (System.Exception e) { Debug.Log("UniqueID property not found.\n" + e.ToString()); }
		}

		protected bool GetIsCurrentProperty(SerializedProperty property)
		{
			try
			{
				if (property.FindPropertyRelative("uniqueID").stringValue == uniqueID.stringValue)
				{
					return true;
				}
			}
			catch (System.Exception e) { e.ToString(); }
			return false;
		}

		protected Rect DrawTopLabel(Rect position)
		{
			//set initial rect
			Rect newPosition = GetStartingPosition(position);
			//draw label
			EditorGUI.LabelField(newPosition, name.stringValue, EditorStyles.boldLabel);
			//add indent
			newPosition = GetIndentedPosition(newPosition);
			return newPosition;
		}

		protected Rect DrawUniqueID(Rect position)
		{
			//draw unique id
			Rect newPosition = GetPosition(position, lineHeight);
			EditorGUI.LabelField(newPosition, "Unique ID", uniqueID.stringValue);
			return newPosition;
		}

		protected virtual void OnAddElement(UnityEditorInternal.ReorderableList list)
		{
			int index = list.serializedProperty.arraySize;
			list.serializedProperty.arraySize++;
			SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);
			element.FindPropertyRelative("uniqueID").stringValue = null;
			element.FindPropertyRelative("name").stringValue = "New Item";
			string serializedUniqueIDPrefix = element.FindPropertyRelative("serializedUniqueIDPrefix").stringValue;
			SerializedProperty uniqueID = element.FindPropertyRelative("uniqueID");
			IProvider<string, string, string> writer = (IProvider<string, string, string>)list.serializedProperty.serializedObject.targetObject;
			uniqueID.stringValue = writer.GetItem(uniqueID.stringValue, serializedUniqueIDPrefix);
		}
	}
}