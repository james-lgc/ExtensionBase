using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSA.Extensions.Base
{
	[System.Serializable]
	[RequireComponent(typeof(UIController))]
	public abstract class ClickableCanvasedManagerBase<T> : CanvasedManagerBase<T> where T : ClickableCanvas
	{
		protected UIController uiController;

		public override void Initialize()
		{
			base.Initialize();
			uiController = GetComponent<UIController>();
			uiController.enabled = false;
		}

		protected override void StartProcess()
		{
			base.StartProcess();
			canvas.DisplayData();
			uiController.enabled = true;
		}

		public override void EndProcess()
		{
			base.EndProcess();
			uiController.enabled = false;
		}
	}
}