using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DSA.Extensions.Base
{
	[Serializable]
	public struct TextValueData
	{
		public string text;
	}

	[UnityEngine.DisallowMultipleComponent]
	public class TextValueComponent : MonoBehaviour
	{
		public string text;
	}
}