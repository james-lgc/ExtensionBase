using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Entities;
using System;

namespace DSA.Extensions.Base
{
	[Serializable]
	public struct VariableRotationData : IComponentData
	{
		public float3 rotationAxis;
		public float3 maxValue;
		public float3 minValue;
	}

	[UnityEngine.DisallowMultipleComponent]
	public class VariableRotationComponent : MonoBehaviour//ComponentDataWrapper<VariableRotationData>
	{
		// public VariableRotationData data;
		public float3 rotationAxis;
		public float3 maxValue;
		public float3 minValue;
	}
}