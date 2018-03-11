using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DSA.Extensions.Base
{
	public class UIController : MonoBehaviour
	{
		public Action CancelAction { get; set; }
		/*public delegate void OnCancelUIEvent();
		public OnCancelUIEvent OnCancelUI;*/

		public void Update()
		{
			CheckCancelUI();
		}

		private void CheckCancelUI()
		{
			if (Input.GetButtonDown("Pause"))
			{
				if (CancelAction != null)
				{
					CancelAction();
				}
			}
			else if (Input.GetButtonDown("ShowJournal"))
			{
				if (CancelAction != null)
				{
					CancelAction();
				}
			}
		}
	}
}