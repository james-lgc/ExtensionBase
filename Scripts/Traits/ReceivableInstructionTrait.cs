using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSA.Extensions.Base
{
	public abstract class ReceivableInstructionTrait : InstructionTrait, IReceivable<InstructionData[]>
	{
		public Func<InstructionData[]> ReceiveFunction { get; set; }

		public override void Use()
		{
			if (ReceiveFunction == null) { return; }
			data = ReceiveFunction();
			if (data == null) { return; }
			base.Use();
			data = null;
		}
	}
}