using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace DSA.Extensions.Base
{
	[System.Serializable]
	public class DataUIButton : UIButtonBase, ISendable<DataItem>, ISettable<DataItem>
	{
		[SerializeField] private bool isSelectableAsClick;
		public override ExtensionEnum Extension { get { return ExtensionEnum.UI; } }

		public Action<DataItem> SendAction { get; set; }

		public DataItem Data { get; protected set; }

		public virtual void Set(DataItem sentData)
		{
			Display(true);
			Data = sentData;
			SetText(Data.Text);
		}

		protected override void Use()
		{
			if (SendAction == null) return;
			SendAction(Data);
		}

		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			if (isSelectableAsClick) { Use(); }
		}
	}
}