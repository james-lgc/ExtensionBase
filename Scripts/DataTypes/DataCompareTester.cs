using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCompareTester : MonoBehaviour
{
	[Header("Operators")] [SerializeField] protected bool isEqual;
	[SerializeField] protected bool isGreater;
	[SerializeField] protected bool isLess;
	[SerializeField] protected bool isGreaterOrEqual;
	[SerializeField] protected bool isLessOrEqual;

	public virtual void CompareData()
	{

	}
}
