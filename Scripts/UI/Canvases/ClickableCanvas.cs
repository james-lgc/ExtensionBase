using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSA.Extensions.Base
{
	public abstract class ClickableCanvas : UICanvas
	{
		public abstract void DisplayData();

		public void SetSelectedDisplayable(int holderIndex, int displayableIndex)
		{
			UIPanel selectedHolder = panels[holderIndex];
			GameObject selectedDisplayable = selectedHolder.Displayables[displayableIndex].ThisGameObject;
			StartCoroutine(SelectionHandler.SelectItem(selectedDisplayable.gameObject));
		}

		public void SetSendActions<T>(System.Action<T> sentAction, int index)
		{
			if (!(panels[index] is ClickableUIPanel)) return;
			ClickableUIPanel panel = (ClickableUIPanel)panels[index];
			panel.SetSendActions<T>(sentAction);
		}

		protected override void Show()
		{
			base.Show();
		}

		protected override void Hide()
		{
			if (this.gameObject.activeSelf)
			{
				try { StartCoroutine(SelectionHandler.SetSelectionToNull()); }
				catch (UnityException e) { Debug.Log(e.ToString()); }
			}
			base.Hide();
		}
	}
}