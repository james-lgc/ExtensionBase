using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using DSA.Extensions.Base;

namespace DSA.Extensions.Base.Editor
{
	public class JsonDataDrawer<T> : ISettable<JsonWriter<T>, string>, IProvider<List<SerializedProperty>>, IPropertyDrawer where T : DataItem
	{
		private JsonWriter<T> writer;
		protected SerializedObject serializedWriter;
		private int currentPropertyID;
		private Vector2 scrollPosition;
		private List<SerializedProperty> propertyList;
		private float lineHeight;

		public void DrawProperty(Rect position)
		{
			//update data from serialized version
			serializedWriter.Update();
			//set rect position
			Rect newPosition = new Rect(position.x + 5F, position.y + 5F, position.width - 10F, 0F);
			//Draw nested structure navigation buttons
			Rect topButtonsPosition = DrawTopButtons(newPosition);
			Rect bottomButtonsPosition = DrawBottomButtons(position);
			float scrollViewHeight = position.height - (topButtonsPosition.height + (bottomButtonsPosition.y - position.height) + 5F);
			//Set currently displayed property
			SerializedProperty currentProperty = propertyList[propertyList.Count - 1];
			Rect propertyPosition = new Rect(newPosition.x, newPosition.y + topButtonsPosition.height, newPosition.width, EditorGUI.GetPropertyHeight(currentProperty));
			Rect scrollArea = new Rect(newPosition.x, newPosition.y + topButtonsPosition.height, newPosition.width, scrollViewHeight);
			scrollPosition = GUI.BeginScrollView(scrollArea, scrollPosition, propertyPosition);
			//draw current property
			EditorGUI.PropertyField(propertyPosition, currentProperty, true);
			//end scroll view
			GUI.EndScrollView();
			//apply changes
			serializedWriter.ApplyModifiedProperties();
		}

		//adds buttons to the top area of the display
		private Rect DrawTopButtons(Rect sentPosition)
		{
			//Create starting putton position
			Rect buttonPosition = new Rect(5F, 5F, 150F, 0F);
			float currentHeight = 0F;
			for (int i = 0; i < propertyList.Count - 1; i++)
			{
				//declare property name variables
				string propertyNameString = null;
				SerializedProperty thisProperty = propertyList[i];
				//find name child property
				propertyNameString = thisProperty.FindPropertyRelative("name").stringValue;
				//create new button
				buttonPosition = new Rect(buttonPosition.x, buttonPosition.y + buttonPosition.height, buttonPosition.width, 20F);
				currentHeight += 20F;
				//create a button for each previous nested levels name
				if (GUI.Button(buttonPosition, propertyNameString))
				{
					//if pressed, reset view to that level
					RemovePropertiesFromList((propertyList.Count - 1) - i);
				}
			}
			//add current height to position
			return new Rect(sentPosition.x, sentPosition.y, sentPosition.width, currentHeight + 5F);
		}

		//adds buttons to the bottom area of the display
		private Rect DrawBottomButtons(Rect position)
		{
			//set button positions
			float buttonHeight = 30F;
			Rect buttonPosition = new Rect(0F, position.height - buttonHeight, position.width, buttonHeight);
			//write to json button
			if (GUI.Button(buttonPosition, "Write to File")) { writer.Process(); }
			//move position up
			buttonPosition = new Rect(0F, buttonPosition.y - buttonHeight, position.width, buttonHeight);
			//read from json button
			if (GUI.Button(buttonPosition, "Read File")) { writer.Set(); }
			// if not at top nested level, create back button
			if (propertyList.Count > 1)
			{
				//move position up
				buttonPosition = new Rect(0F, buttonPosition.y - buttonHeight, position.width, buttonHeight);
				//if button pressed, reset view to previous nested level
				if (GUI.Button(buttonPosition, "Back")) { RemovePropertiesFromList(); };
			}
			return buttonPosition;
		}

		//removes a given number of properties from viewing history
		protected void RemovePropertiesFromList(int i = 0)
		{
			if (propertyList.Count > 1 && propertyList.Count >= i)
			{
				//remove from property list at index
				propertyList.RemoveAt(propertyList.Count - 1);
			}
			//step i down
			i += -1;
			//if i still greater than 0, repeat process
			if (i > 0) { RemovePropertiesFromList(i); }
		}

		//ensure all properties are set
		public void Set(JsonWriter<T> sentWriter, string sentName)
		{
			writer = sentWriter;
			serializedWriter = new SerializedObject(writer);
			lineHeight = EditorGUIUtility.singleLineHeight;
			if (propertyList == null)
			{
				propertyList = new List<SerializedProperty>();
				SerializedProperty serializedConversationList = serializedWriter.FindProperty(sentName);
				propertyList.Add(serializedConversationList);
			}
		}

		public List<SerializedProperty> GetItem()
		{
			if (propertyList == null)
			{
				propertyList = new List<SerializedProperty>();
			}
			return propertyList;
		}
	}
}