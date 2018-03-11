using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSA.Extensions.Base
{
	[System.Serializable]
	public class HUDCanvas : UICanvas
	{
		public override ExtensionEnum.Extension Extension { get { return ExtensionEnum.Extension.GamePlay; } }

		[SerializeField] private InteractHUD interactHUD;

		protected override void Hide()
		{
			base.Hide();
			HideInteractHUD();
		}

		public void ShowInteractHUD(GameObject viewedObject)
		{
			interactHUD.Show(viewedObject);
		}

		public void HideInteractHUD()
		{
			interactHUD.Hide();
		}


	}
}