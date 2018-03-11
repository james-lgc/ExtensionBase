using UnityEngine;
using UnityEditor;
using DSA.Extensions.Base;

namespace DSA.Extensions.Base.Editor
{
	//base class for custom property drawers
	//contains formatting information and methods to return formatted positons
	public abstract class BasePropertyDrawer : PropertyDrawer
	{
		//height of a single line
		protected float lineHeight = EditorGUIUtility.singleLineHeight;
		//horiztonal length to indent by
		protected float indentDistance { get { return 30F; } }
		//extra space added between elements
		protected float verticalPadding { get { return 5F; } }
		//initial extra space at top of property
		protected float initialVerticalPaddingHeight { get { return 10F; } }
		//initial extra spac at side of property
		protected float initialHorizontalPadding { get { return 5F; } }

		//method to ensure all properties are set
		protected abstract void SetProperties(SerializedProperty sentProperty);
		//method to draw a properties members onto a supplied rect
		protected abstract void DrawChildProperties(Rect position, SerializedProperty property);

		//returns the default starting position
		//includes padding
		protected Rect GetStartingPosition(Rect sentPosition, bool usePadding = true)
		{
			float horiztonalPad = 0F;
			//if indented, add padding
			if (usePadding == true) { horiztonalPad = initialHorizontalPadding; }
			float verticalPad = 0F;
			//if extra vertical space needed, add padding
			if (usePadding == true) { verticalPad = initialVerticalPaddingHeight; }
			//create and return new rect
			return new Rect(sentPosition.x, sentPosition.y + verticalPad, sentPosition.width - horiztonalPad, lineHeight);
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

		//sent as a func to list draw element call back
		//shows how many stages in each conversation element
		protected string GetArrayCountString(SerializedProperty sentParent, string arrayPropertyName, string singleUnit, string pluralUnit)
		{
			SerializedProperty array = sentParent.FindPropertyRelative(arrayPropertyName);
			int length = array.arraySize;
			string unitString = " " + pluralUnit + "]";
			if (length == 1) { unitString = " " + singleUnit + "]"; }
			return "[" + length + unitString;
		}

		//returns a new rect from an old rect and a new height
		protected
		 Rect GetPosition(Rect sentPosition, float sentHeight, bool isIndented = false, bool usePadding = true)
		{
			float thisIndent = 0F;
			//if indented, add padding
			if (isIndented == true) { thisIndent = indentDistance; }
			float thisVeritcalPadding = 0F;
			//if extra vertical space needed, add padding
			if (usePadding == true) { thisVeritcalPadding = verticalPadding; }
			//create and return new rect
			return new Rect(sentPosition.x + thisIndent, sentPosition.y + sentPosition.height + thisVeritcalPadding, sentPosition.width - thisIndent, sentHeight);
		}

		//returns an indented position
		protected Rect GetIndentedPosition(Rect sentPosition, bool isIndented = true)
		{
			//set positive indent
			float thisIndent = indentDistance;
			//if not indeted, set negative indent distance
			if (!isIndented) { thisIndent = -indentDistance; }
			//return new position
			return new Rect(sentPosition.x + thisIndent, sentPosition.y, sentPosition.width - thisIndent, sentPosition.height);
		}

		//returns a rect tailored to reorderable lists
		protected Rect GetListPosition(Rect sentPosition)
		{
			return new Rect(sentPosition.x + 30F, sentPosition.y, sentPosition.width - 30F, sentPosition.height);
		}

		//returns the total height taken with padding
		protected float GetAddedHeight(float sentHeight)
		{
			return sentHeight + verticalPadding;
		}
		//Get the height of a property
		protected float GetHeight(SerializedProperty sentProperty)
		{
			return EditorGUI.GetPropertyHeight(sentProperty, true);
		}

		protected Rect DrawTopLabel(Rect position, string label, bool usePadding = true)
		{
			//set initial rect
			Rect newPosition = GetStartingPosition(position, usePadding);
			//draw label
			EditorGUI.LabelField(newPosition, label, EditorStyles.boldLabel);
			//add indent
			newPosition = GetIndentedPosition(newPosition);
			return newPosition;
		}

		protected Rect DrawLabel(Rect position, string label, bool isIndented = true)
		{
			//set initial rect
			Rect newPosition = GetPosition(position, lineHeight);
			//draw label
			EditorGUI.LabelField(newPosition, label, EditorStyles.boldLabel);
			//add indent
			if (isIndented)
			{
				newPosition = GetIndentedPosition(newPosition);
			}
			return newPosition;
		}

		protected Rect DrawTextField(Rect position, SerializedProperty sentProperty, string label)
		{
			Rect newPosition = GetPosition(position, lineHeight);
			sentProperty.stringValue = EditorGUI.TextField(newPosition, label, sentProperty.stringValue);
			return newPosition;
		}

		protected Rect DrawTextArea(Rect position, SerializedProperty sentProperty, string label)
		{
			EditorStyles.textArea.wordWrap = true;
			Rect newPosition = GetPosition(position, GetHeight(sentProperty));
			Rect textPosition = EditorGUI.PrefixLabel(newPosition, GUIUtility.GetControlID(FocusType.Passive), new GUIContent(label));
			sentProperty.stringValue = EditorGUI.TextArea(textPosition, sentProperty.stringValue, EditorStyles.textArea);
			return newPosition;
		}

		protected Rect DrawIntField(Rect position, SerializedProperty sentProperty, string label)
		{
			Rect newPosition = GetPosition(position, lineHeight);
			sentProperty.intValue = EditorGUI.IntField(newPosition, label, sentProperty.intValue);
			return newPosition;
		}

		protected Rect DrawBoolField(Rect position, SerializedProperty sentProperty, string label)
		{
			Rect newPosition = GetPosition(position, lineHeight);
			sentProperty.boolValue = EditorGUI.Toggle(newPosition, label, sentProperty.boolValue);
			return newPosition;
		}

		protected Rect DrawPropertyField(Rect position, SerializedProperty sentProperty, string label = null, bool usePadding = true)
		{
			Rect newPosition = GetPosition(position, GetHeight(sentProperty), usePadding: usePadding);
			GUIContent content = null;
			if (!string.IsNullOrEmpty(label))
			{
				content = new GUIContent(label);
			}
			EditorGUI.PropertyField(newPosition, sentProperty, content, true);
			//EditorGUI.DrawRect(position, Color.blue);
			return newPosition;
		}

		protected Rect DrawReorderableList(Rect position, UnityEditorInternal.ReorderableList sentList, string label)
		{
			//draw label
			Rect newPosition = position;
			if (!string.IsNullOrEmpty(label))
			{
				newPosition = GetPosition(newPosition, lineHeight);
				EditorGUI.LabelField(newPosition, label, EditorStyles.boldLabel);
			}
			//draw list
			newPosition = GetPosition(newPosition, sentList.GetHeight());
			Rect listPosition = GetListPosition(newPosition);
			sentList.DoList(listPosition);
			sentList.draggable = true;
			return newPosition;
		}

		//Returns a reorderable list of properties which are fully displayed 
		protected UnityEditorInternal.ReorderableList GetDefaultFullDisplayList(SerializedProperty sentProperty, string sentLabel, bool allowMultipleExpansions = false)
		{
			//create reorderable list
			UnityEditorInternal.ReorderableList tempList = new UnityEditorInternal.ReorderableList(sentProperty.serializedObject, sentProperty, true, true, true, true);
			tempList.drawHeaderCallback = (Rect rect) =>
			{
				//Draw header
				EditorGUI.LabelField(rect, sentLabel);
			};
			//Set how list elements are drawn
			tempList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
			{
				SerializedProperty element = tempList.serializedProperty.GetArrayElementAtIndex(index);
				float elementHeight = GetHeight(element);
				rect = new Rect(rect.x, rect.y, rect.width, elementHeight + 5);
				EditorGUI.PropertyField(rect, element, true);
			};
			//calculate element height individually
			tempList.elementHeightCallback = (index) =>
			{
				SerializedProperty element = tempList.serializedProperty.GetArrayElementAtIndex(index);
				float elementHeight = EditorGUI.GetPropertyHeight(element, true);
				elementHeight += 5F;
				return elementHeight;
			};
			return tempList;
		}

		//returns a reorderable list with an edit button linked to an editor window
		protected UnityEditorInternal.ReorderableList GetDefaultEditButtonList(SerializedProperty sentProperty, string sentLabel, System.Action<SerializedProperty> editAction, System.Func<SerializedProperty, string> endTextFunc = null, System.Action<UnityEditorInternal.ReorderableList> addAction = null)
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
				float useableXPosition = rect.x + totalButtonWidth;
				float endLength = 5F;
				float minimumMainTextWidth = 60F;
				EditorStyles.label.clipping = TextClipping.Clip;
				if (endTextFunc != null)
				{
					float desiredEndLeghth = 80F;
					if (rect.width - (totalButtonWidth + desiredEndLeghth + 10F) >= minimumMainTextWidth)
					{
						endLength = desiredEndLeghth;
					}
					else
					{
						endLength = rect.width - (totalButtonWidth + desiredEndLeghth + 10F);
					}
					if (endLength < 0F) { endLength = 0F; }
					Rect endTextRect = new Rect(rect.x + rect.width - endLength - 5F, rect.y, endLength, rect.height);
					EditorGUI.LabelField(endTextRect, endTextFunc(element), EditorStyles.label);
				}
				try
				{
					//Display a label on the element with the element name
					//relies on element having name property
					float textWidth = minimumMainTextWidth;
					if (minimumMainTextWidth < rect.width - (totalButtonWidth))
					{
						textWidth = rect.width - (totalButtonWidth + endLength + 10F);
					}
					else
					{
						textWidth = rect.width - (totalButtonWidth + endLength + 10F);
					}
					Rect textRect = new Rect(useableXPosition, rect.y, textWidth, EditorGUIUtility.singleLineHeight);
					EditorGUI.LabelField(textRect, element.FindPropertyRelative("name").stringValue, EditorStyles.label);
				}

				//if no name property found, log an error message
				catch (System.Exception e)
				{
					Debug.Log("No name property found in list element.\n" + e.ToString());
				}
				//change colours for testing
			};
			//implement onAdd
			if (addAction != null)
			{
				reorderableList.onAddCallback = (UnityEditorInternal.ReorderableList list) =>
				{
					addAction(list);
				};
			}
			return reorderableList;
		}
	}
}