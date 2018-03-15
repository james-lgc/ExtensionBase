using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSA.Extensions.Base
{
	public abstract class InstructionTrait : TraitBase, ISendable<InstructionData[]>, IUseable
	{
		[SerializeField] protected InstructionData[] data;
		public override ExtensionEnum Extension { get { return ExtensionEnum.None; } }

		public Action<InstructionData[]> SendAction { get; set; }

		public virtual void Use()
		{
			if (SendAction == null) { return; }
			SendAction(data);
		}
	}
}