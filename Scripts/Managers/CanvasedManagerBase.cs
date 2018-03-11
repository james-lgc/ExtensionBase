using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSA.Extensions.Base
{
	[System.Serializable]
	public abstract class CanvasedManagerBase<T> : ManagerBase where T : UICanvas
	{
		[Header("Canvas")] [SerializeField] protected T canvas;
		public T Canvas { get { return canvas; } }
		private bool isBaseDisplay = true;

		public override void Initialize()
		{
			base.Initialize();
			canvas.Initialize();
			canvas.SetEndAction(EndProcess);
			RaiseTraitsFound(canvas);
		}

		public override void LateInitialize()
		{
			canvas.Display(false);
		}

		protected override void StartProcess()
		{
			ShowCanvas();
			base.StartProcess();
		}

		public override void EndProcess()
		{
			HideCanvas();
			base.EndProcess();
		}

		public virtual void ShowCanvas()
		{
			RaiseStartEvent();
			canvas.Display(true);
		}

		public virtual void HideCanvas()
		{
			canvas.Display(false);
			RaiseEndEvent();
		}
	}
}