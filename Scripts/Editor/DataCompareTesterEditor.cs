using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DSA.Extensions.Base;

namespace DSA.Extensions.Base.Editor
{
	[CustomEditor(typeof(DataCompareTester), true)]
	[CanEditMultipleObjects]
	public class DataCompareTesterEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			DataCompareTester compareTester = (DataCompareTester)target;
			if (GUILayout.Button("Compare Data"))
			{
				compareTester.CompareData();
			}
		}
	}
}