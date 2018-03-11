using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSA.Extensions.Base
{
	//T is UI type, U is data type
	public class UIPanel : MonoBehaviour
	{
		private IDisplayable[] displayables;
		public IDisplayable[] Displayables { get { return displayables; } }
		private IDisplayable[] activeDisplayables;

		public virtual void PopulateDisplayables()
		{
			displayables = GetDirectChildren<IDisplayable>(this.gameObject);
			if (displayables != null)
			{
				for (int i = 0; i < displayables.Length; i++)
				{
					displayables[i].Initialize();
				}
			}
		}

		private T[] GetDirectChildren<T>(GameObject sentParent)
		{
			List<T> tempList = new List<T>();
			foreach (Transform trans in sentParent.transform)
			{
				try
				{
					T thisT = trans.GetComponent<T>();
					tempList.Add(thisT);
				}
				catch (UnityException e) { e.ToString(); }
			}
			T[] tArray = tempList.ToArray();
			return tArray;
		}

		public void SetData<T>(T[] sentData)
		{
			if (sentData == null) { HideDisplayables(); return; }
			if (sentData.Length == 0) { HideDisplayables(); return; }
			gameObject.SetActive(true);
			for (int i = 0; i < displayables.Length; i++)
			{
				if (i < sentData.Length)
				{
					displayables[i].Display(true);
					if (displayables[i] is ISettable<T>)
					{
						ISettable<T> settable = (ISettable<T>)displayables[i];
						settable.Set(sentData[i]);
					}
				}
				else
				{
					displayables[i].Clear();
				}
			}
		}

		public void Clear()
		{
			if (displayables == null) { return; }
			for (int i = 0; i < displayables.Length; i++)
			{
				displayables[i].Clear();
			}
		}

		private void HideDisplayables()
		{
			gameObject.SetActive(false);
			if (displayables == null) return;
			for (int i = 0; i < displayables.Length; i++)
			{
				displayables[i].Clear();
			}
		}

		public void DisplayAll()
		{
			if (displayables != null)
			{
				for (int i = 0; i < displayables.Length; i++)
				{
					//displayables[i].Clear();
					displayables[i].Display(true);
				}
			}
		}

		public IDisplayable GetFirstDisplayable()
		{
			return displayables[0];
		}

	}
}