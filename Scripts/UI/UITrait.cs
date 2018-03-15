using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace DSA.Extensions.Base
{
	[RequireComponent(typeof(TraitedMonoBehaviour))]
	[System.Serializable]
	public abstract class UITrait : TraitBase
	{
		public override ExtensionEnum Extension { get { return ExtensionEnum.UI; } }
	}
}