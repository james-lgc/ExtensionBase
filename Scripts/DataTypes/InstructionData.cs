using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSA.Extensions.Base
{
	[System.Serializable]
	public struct InstructionData
	{
		public ExtensionEnum extension;
		public string name;
		public int[] identifier;
		public TransformValue transformValue;

		public InstructionData(ExtensionEnum sentExtension = ExtensionEnum.None, string sentName = null, int[] sentIdentifer = null, TransformValue sentTransformValue = default(TransformValue))
		{
			extension = sentExtension;
			name = sentName;
			identifier = sentIdentifer;
			transformValue = sentTransformValue;
		}
	}
}