using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSA.Extensions.Base
{
	[AddComponentMenu("Trait/Base/(Sequenced) Receivable Instruction Trait")]
	[System.Serializable]
	public class SequencedReceivableInstructionTrait : ReceivableInstructionTrait, ISequenceable
	{
		[SerializeField] private int sequenceOrder;

		public void CallInSequence(int sequenceID)
		{
			if (sequenceID == sequenceOrder)
			{
				Use();
			}
		}
	}
}