using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DSA.Extensions.Base;

namespace DSA.Extensions.Base.Editor
{
	public abstract class DataEditorWindow<T> : EditorWindow, ISettable<JsonWriter<T>, string> where T : DataItem
	{
		protected JsonWriter<T> writer;
		protected SerializedObject serializedWriter;
		protected int currentPropertyID;
		protected Vector2 scrollPosition;
		protected List<SerializedProperty> propertyList;
		protected float lineHeight;
		protected abstract int maxNestedButtons { get; }
		protected string bannerTitle;

		void OnGUI()
		{
			Rect newPosition = new Rect(position.x - position.x, position.y - position.y, position.width, position.height);
			newPosition = new Rect(newPosition.x + 5F, newPosition.y + 5F, newPosition.width - 10F, 0F);
			//update data from serialized version
			serializedWriter.Update();
			//Draw nested structure navigation buttons
			Rect topButtonsPosition = DrawTopButtons(newPosition);
			Rect bottomButtonsPosition = DrawBottomButtons(position);
			float scrollViewHeight = position.height - (topButtonsPosition.height + bottomButtonsPosition.height + 5F);
			//Set currently displayed property
			SerializedProperty currentProperty = propertyList[propertyList.Count - 1];
			Rect propertyPosition = new Rect(newPosition.x, newPosition.y + topButtonsPosition.height, newPosition.width, EditorGUI.GetPropertyHeight(currentProperty));
			Rect scrollArea = new Rect(newPosition.x, newPosition.y + topButtonsPosition.height, newPosition.width, scrollViewHeight);
			scrollPosition = GUI.BeginScrollView(scrollArea, scrollPosition, propertyPosition);
			//draw current property
			currentProperty.FindPropertyRelative("isExpanded").boolValue = true;
			EditorGUI.PropertyField(propertyPosition, currentProperty, true);
			currentProperty.FindPropertyRelative("isExpanded").boolValue = true;
			//Debug.Log("Property Expansion: " + currentProperty.FindPropertyRelative("isExpanded").boolValue);
			//end scroll view
			GUI.EndScrollView();
			//apply changes
			serializedWriter.ApplyModifiedProperties();
		}

		public void DrawProperty(Rect position)
		{
			//update data from serialized version
			serializedWriter.Update();
			//set rect position
			Rect newPosition = new Rect(position.x - position.x + 5F, position.y - position.y + 5F, position.width - 10F, 0F);
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
			float singleButtonHeight = 20F;
			float totalHeight = (singleButtonHeight * maxNestedButtons) + lineHeight * 3;
			Rect totalPosition = new Rect(sentPosition.x, sentPosition.y, sentPosition.width, totalHeight);
			EditorGUI.DrawRect(totalPosition, GetEditorColor());

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
				buttonPosition = new Rect(buttonPosition.x, buttonPosition.y + buttonPosition.height, buttonPosition.width, singleButtonHeight);
				currentHeight += 20F;
				GUIStyle style = new GUIStyle(GUI.skin.button);
				style.alignment = TextAnchor.MiddleLeft;
				//create a button for each previous nested levels name
				string buttonString = "[" + thisProperty.type + "] " + propertyNameString;
				if (GUI.Button(buttonPosition, buttonString, style))
				{
					//if pressed, reset view to that level
					RemovePropertiesFromList((propertyList.Count - 1) - i);
				}
			}
			for (int i = propertyList.Count; i <= maxNestedButtons; i++)
			{
				GUIStyle style = new GUIStyle(GUI.skin.horizontalSlider);
				style.alignment = TextAnchor.MiddleLeft;
				buttonPosition = new Rect(buttonPosition.x, buttonPosition.y + buttonPosition.height, buttonPosition.width, singleButtonHeight);
				EditorGUI.LabelField(buttonPosition, "", style);
			}
			Rect linePosition = new Rect(sentPosition.x, sentPosition.y + totalHeight - (lineHeight * 3), sentPosition.width, lineHeight);
			EditorGUI.LabelField(linePosition, "", GUI.skin.horizontalSlider);

			Rect bannerTitlePosition = new Rect(sentPosition.x, totalHeight - (lineHeight * 2 - 5F), sentPosition.width, lineHeight);
			bannerTitle = propertyList[propertyList.Count - 1].type;
			EditorGUI.LabelField(bannerTitlePosition, bannerTitle, EditorStyles.boldLabel);

			linePosition = new Rect(sentPosition.x, sentPosition.y + totalHeight - (lineHeight), sentPosition.width, lineHeight);
			EditorGUI.LabelField(linePosition, "", GUI.skin.horizontalSlider);
			//add current height to position
			return totalPosition;
		}

		//adds buttons to the bottom area of the display
		private Rect DrawBottomButtons(Rect position)
		{
			//set button positions
			float buttonHeight = 30F;
			float totalHeight = buttonHeight * 3 + lineHeight;
			Rect totalPosition = new Rect(0F, position.height - totalHeight, position.width, totalHeight);
			EditorGUI.DrawRect(totalPosition, GetEditorColor());
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
			Rect linePosition = new Rect(0F, totalPosition.y, position.width, lineHeight);
			EditorGUI.LabelField(linePosition, "", GUI.skin.horizontalSlider);
			totalHeight += lineHeight;
			return totalPosition;
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
			if (propertyList == null || propertyList.Count == 0)
			{
				propertyList = new List<SerializedProperty>();
				SerializedProperty serializedConversationList = serializedWriter.FindProperty(sentName);
				propertyList.Add(serializedConversationList);
			}
		}

		protected Color GetEditorColor()
		{
			Color colour = EditorGUIUtility.isProSkin ? (Color)new Color32(56, 56, 56, 255) : (Color)new Color32(194, 194, 194, 255);
			return colour;
		}
	}
}