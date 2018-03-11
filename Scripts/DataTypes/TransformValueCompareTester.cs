using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformValueCompareTester : DataCompareTester
{
	[Header("Data")] [SerializeField] private TransformValue transformValue1;
	[SerializeField] private TransformValue transformValue2;

	public override void CompareData()
	{
		isEqual = transformValue1 == transformValue2;
	}
}
