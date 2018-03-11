using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace DSA.Extensions.Base
{
	public class SelectionHandler : MonoBehaviour
	{
		//Might not need to be Monobehavour
		//private Button flashingText;
		//private bool textVisible = true;
		//private int timeCounter;
		[SerializeField] private GameObject currentObj;

		private void Awake()
		{
			//Send currentObj to string to remove Unity compile warning.
			currentObj.ToString();
		}

		private void Update()
		{
			currentObj = EventSystem.current.currentSelectedGameObject;
		}

		public static IEnumerator SelectFirst(Button[] buttons)
		{
			try { EventSystem.current.SetSelectedGameObject(null); }
			catch (System.Exception e) { e.ToString(); }
			yield return new WaitForEndOfFrame();
			if (EventSystem.current.currentSelectedGameObject == null)
			{
				if (buttons != null)
				{
					if (buttons.Length > 0 && buttons[0] != null)
					{
						EventSystem.current.SetSelectedGameObject(buttons[0].gameObject);
					}
				}
			}
		}

		public static IEnumerator SelectItem(GameObject gameObj)
		{
			try { EventSystem.current.SetSelectedGameObject(null); }
			catch (System.Exception e) { e.ToString(); }
			yield return new WaitForEndOfFrame();
			EventSystem.current.SetSelectedGameObject(gameObj);
		}

		public static IEnumerator SetSelectionToNull()
		{
			try { EventSystem.current.SetSelectedGameObject(null); }
			catch (System.Exception e) { e.ToString(); }
			yield return new WaitForEndOfFrame();
			EventSystem.current.SetSelectedGameObject(null);
		}


	}
}