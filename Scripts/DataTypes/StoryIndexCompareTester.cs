using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryIndexCompareTester : DataCompareTester
{
	[Header("Data")] [SerializeField] private StoryIndex storyIndex1;
	[SerializeField] private StoryIndex storyIndex2;

	[ExecuteInEditMode]
	public override void CompareData()
	{
		isEqual = storyIndex1 == storyIndex2;
		isGreater = storyIndex1 > storyIndex2;
		isLess = storyIndex1 < storyIndex2;
		isGreaterOrEqual = storyIndex1 >= storyIndex2;
		isLessOrEqual = storyIndex1 <= storyIndex2;
	}
}
