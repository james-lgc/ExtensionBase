using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace DSA.Extensions.Base
{
	public class OnSpawnSelected : MonoBehaviour, ISubmitHandler
	{

		void Awake()
		{
			EventSystem.current.SetSelectedGameObject(this.gameObject);
		}

		public void OnSubmit(BaseEventData eventdata)
		{
			//GameManager gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
			//gameManager.BeginDialogue ();
			//Destroy(this.gameObject);
		}

	}
}