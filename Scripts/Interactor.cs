using UnityEngine;
using System.Collections;

namespace DSA.Extensions.Base
{
	public class Interactor
	{

		private int mask;

		public void InteractWith(GameObject viewedObject)
		{
			foreach (MonoBehaviour behaviour in viewedObject.GetComponents<MonoBehaviour>())
			{
				if (behaviour is IInteractable)
				{
					IInteractable interactable = (IInteractable)behaviour;
					interactable.Interact();
				}
			}
			return;
		}

		public GameObject GetInteractable(Transform trans)
		{
			mask = 1 << 8;
			Ray ray = new Ray(trans.position, trans.forward);
			//Debug.DrawRay(trans.position, trans.forward, Color.cyan, 100); //right instead of forward due to blender axis import
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 1.5F, mask))
			{
				//Debug.Log("hit: " + hit.collider.gameObject.name);
				//Debug.DrawRay();
				return hit.collider.gameObject;
			}
			else
			{
				return null;
			}
		}
	}
}