using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DSA.Extensions.Base;
using System.Reflection;

namespace DSA.Extensions.Base.Editor
{
	public abstract class DataItemDrawer : BasePropertyDrawer
	{
		protected SerializedProperty name;
		protected SerializedProperty id;
		protected SerializedProperty uniqueID;
		protected abstract System.Action<SerializedProperty> editAction { get; }

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

		protected string GetLabelText(SerializedProperty sentProperty)
		{
			string propertyPath = sentProperty.propertyPath;
			System.Type rootType = sentProperty.serializedObject.targetObject.GetType();
			var field = rootType.GetField(sentProperty.propertyPath);
			DataItem item = (DataItem)EditorTool.GetReflectedObjectFromPath(rootType, propertyPath, sentProperty.serializedObject.targetObject);
			return item.GetLabelText();
		}

		protected string GetLabelEndText(SerializedProperty sentProperty)
		{
			string propertyPath = sentProperty.propertyPath;
			System.Type rootType = sentProperty.serializedObject.targetObject.GetType();
			DataItem item = (DataItem)EditorTool.GetReflectedObjectFromPath(rootType, propertyPath, sentProperty.serializedObject.targetObject);
			return item.GetEndLabelText();
		}

		//called multiple times per frame by Unity while UI is displayed
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			//refresh display from serialized data
			property.serializedObject.Update();
			//Ensure all properties set
			SetProperties(property);
			//Draw ui elements
			DrawChildProperties(position, property);
			//Apply any changes
			property.serializedObject.ApplyModifiedProperties();
		}

		protected void DrawHeader(Rect position, SerializedProperty property)
		{
			float useableXPosition = position.x;
			float endLength = 5F;
			float minimumMainTextWidth = 60F;
			EditorStyles.label.clipping = TextClipping.Clip;
			if (GetLabelEndText(property) != null)
			{
				float desiredEndLeghth = 250F;
				if (position.width - (desiredEndLeghth + 10F) >= minimumMainTextWidth)
				{
					endLength = desiredEndLeghth;
				}
				else
				{
					endLength = position.width - (desiredEndLeghth + 10F);
				}
				if (endLength < 0F) { endLength = 0F; }
				Rect endTextRect = new Rect(position.x + position.width - endLength - 5F, position.y, endLength, position.height);
				EditorGUI.LabelField(endTextRect, GetLabelEndText(property), EditorStyles.label);
			}
			try
			{
				//Display a label on the element with the element name
				//relies on element having name property
				float textWidth = minimumMainTextWidth;
				if (minimumMainTextWidth < position.width - 5F)
				{
					textWidth = position.width - (endLength + 10F);
				}
				else
				{
					textWidth = position.width - (endLength + 10F);
				}
				Rect textRect = new Rect(useableXPosition, position.y, textWidth, EditorGUIUtility.singleLineHeight);
				EditorGUI.LabelField(textRect, GetLabelText(property), EditorStyles.label);
			}
			//if no name property found, log an error message
			catch (System.Exception e)
			{
				Debug.Log("No name property found in list element.\n" + e.ToString());
			}
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
			Rect newPosition = EditorTool.GetStartingPosition(position);
			//draw label
			EditorGUI.LabelField(newPosition, name.stringValue, EditorStyles.boldLabel);
			//add indent
			newPosition = EditorTool.GetIndentedPosition(newPosition);
			return newPosition;
		}

		protected Rect DrawUniqueID(Rect position)
		{
			//draw unique id
			Rect newPosition = EditorTool.GetPosition(position, EditorTool.LineHeight);
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

		//returns a reorderable list with an edit button linked to an editor window
		protected UnityEditorInternal.ReorderableList GetDefaultEditButtonList(SerializedProperty sentProperty, string sentLabel)
		{
			//create reorderable list
			UnityEditorInternal.ReorderableList reorderableList = new UnityEditorInternal.ReorderableList(sentProperty.serializedObject, sentProperty, true, true, true, true);
			reorderableList.drawHeaderCallback = (Rect rect) =>
			{
				//Draw header
				EditorGUI.LabelField(rect, sentLabel);
			};
			//Set how list elements are drawn
			reorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
			{
				//Get the property from the element
				SerializedProperty element = reorderableList.serializedProperty.GetArrayElementAtIndex(index);
				//Add extra height padding
				rect.y += 2;
				//display edit button
				if (GUI.Button(new Rect(rect.x + 10, rect.y, 60, EditorGUIUtility.singleLineHeight), "Edit"))
				{
					//if pressed, call edit action
					editAction(element);
				}
				float totalButtonWidth = 80F;
				Rect newPosition = new Rect(rect.x + totalButtonWidth, rect.y, rect.width - totalButtonWidth, rect.height);
				EditorStyles.label.clipping = TextClipping.Clip;
				DrawHeader(newPosition, element);
			};
			//implement onAdd
			reorderableList.onAddCallback = (UnityEditorInternal.ReorderableList list) =>
			{
				OnAddElement(list);
			};
			return reorderableList;
		}

		protected string GetElementChildCountText(SerializedProperty sentElement)
		{
			return null;
		}
	}
}