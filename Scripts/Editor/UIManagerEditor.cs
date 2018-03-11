using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DSA.Extensions.Base;

namespace DSA.Extensions.Base.Editor
{
	[CustomEditor(typeof(UIManager))]
	public class UIManagerEditor : UnityEditor.Editor
	{
		//GUI Buttons to update all UI elements to global ui colorscheme
		public override void OnInspectorGUI()
		{
			UIManager uiManager = (UIManager)target;
			DrawDefaultInspector();
			if (GUILayout.Button("Update Panel Colours"))
			{
				uiManager.SetUIElements();
				Undo.RecordObjects(uiManager.Panels, "Updated Panel Colours");
				uiManager.UpdatePanelColours();
			}
			if (GUILayout.Button("Update Text Colours"))
			{
				uiManager.SetUIElements();
				Undo.RecordObjects(uiManager.Texts, "Updated Text Colours");
				uiManager.UpdateTextColours();
			}
			if (GUILayout.Button("Update Button Colours"))
			{
				uiManager.SetUIElements();
				Undo.RecordObjects(uiManager.Buttons, "Updated Button Colours");
				uiManager.UpdateButtonColours();
			}
			if (GUILayout.Button("Update Text Fonts"))
			{
				uiManager.SetUIElements();
				Undo.RecordObjects(uiManager.Texts, "Updated Text Fonts");
				uiManager.UpdateTextFonts();
			}
		}
	}
}