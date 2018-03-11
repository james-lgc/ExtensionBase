using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DSA.Extensions.Base
{
	public class InteractHUD : MonoBehaviour
	{
		[SerializeField] Text interactText;
		[SerializeField] Text nameText;

		public void Show(GameObject viewedObject)
		{
			nameText.text = viewedObject.name;
			gameObject.SetActive(true);
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}

	}
}