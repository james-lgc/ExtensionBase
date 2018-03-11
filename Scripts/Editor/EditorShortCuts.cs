using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorShortCuts : EditorWindow
{
	[MenuItem("Window/LayoutShortcuts/Preferred %1", false, 999)]
	static void Layout1()
	{
		EditorApplication.ExecuteMenuItem("Window/Layouts/Preferred Layout");
	}

	[MenuItem("Window/LayoutShortcuts/Data Editing %2", false, 999)]
	static void Layout2()
	{
		EditorApplication.ExecuteMenuItem("Window/Layouts/Data Editing");
	}
}
