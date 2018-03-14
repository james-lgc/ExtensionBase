using UnityEngine;
using UnityEditor;
using DSA.Extensions.Base;

namespace DSA.Extensions.Base.Editor
{
	//base class for custom property drawers
	//contains formatting information and methods to return formatted positons
	public abstract class BasePropertyDrawer : PropertyDrawer
	{
		//method to ensure all properties are set
		protected abstract void SetProperties(SerializedProperty sentProperty);
		//method to draw a properties members onto a supplied rect
		protected abstract void DrawChildProperties(Rect position, SerializedProperty property);

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
	}
}