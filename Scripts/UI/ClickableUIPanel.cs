using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSA.Extensions.Base
{
	public class ClickableUIPanel : UIPanel
	{
		public void SetSendActions<T>(System.Action<T> sentAction)
		{
			for (int i = 0; i < Displayables.Length; i++)
			{
				if (!(Displayables[i] is ISendable<T>)) return;
				ISendable<T> button = (ISendable<T>)Displayables[i];
				button.SendAction = sentAction;
			}
		}
	}
}