using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSA.Extensions.Base
{
	[RequireComponent(typeof(TraitedMonoBehaviour))]
	[System.Serializable]
	public abstract class UICanvasTrait : TraitBase, ISendable<UICanvas>
	{
		public override ExtensionEnum.Extension Extension { get { return ExtensionEnum.Extension.UI; } }

		public Action<UICanvas> SendAction { get; set; }

		private UICanvas data;
		public UICanvas Data { get { return data; } protected set { data = value; } }

		public void SetData(UICanvas sentCanvas)
		{
			data = sentCanvas;
		}

		protected void Use()
		{
			if (!GetIsExtensionLoaded() || SendAction == null || Data == null) { return; }
			SendAction(Data);
		}
	}
}