using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Mathematics;
using Unity.Collections;

namespace DSA.Extensions.Base
{
	[Serializable]
	public struct VariableTextureScaleData
	{
		public float2 max;
		public float2 min;
		public int materialIndex;
	}

	[UnityEngine.DisallowMultipleComponent]
	public class VariableTextureOffsetComponent : MonoBehaviour
	{
		public float2 max;
		public float2 min;
		public int materialIndex;
	}
}