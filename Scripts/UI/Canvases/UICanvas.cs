using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

namespace DSA.Extensions.Base
{
	[RequireComponent(typeof(ActivateableUICanvasTrait))]
	[System.Serializable]
	public abstract class UICanvas : TraitedMonoBehaviour, IDirectable<UICanvas>
	{
		[SerializeField] private int id;
		public int ID { get { return id; } }
		[SerializeField] bool isOverrideable;
		public bool IsOverrideable { get { return isOverrideable; } }
		[SerializeField] protected UIPanel[] panels;
		public UIPanel[] Panels { get { return panels; } }

		private UICanvasTrait canvasTrait;

		[SerializeField] private IPrintable[] dataArray;
		public IPrintable[] DataArray { get { return dataArray; } }

		public GameObject ThisGameObject { get { return gameObject; } }

		public Action<UICanvas> DirectAction { get; set; }

		public Action EndAction;

		public virtual void Initialize()
		{
			Show();
			PopulatePanels();
			canvasTrait = GetComponent<UICanvasTrait>();
			if (canvasTrait == null) { return; }
			canvasTrait.SetData(this);
		}

		private void OnEnable()
		{
			Activate();
		}

		public void SetEndAction(Action sentAction)
		{
			EndAction = sentAction;
		}

		protected virtual void PopulatePanels()
		{
			if (panels == null) { return; }
			for (int i = 0; i < panels.Length; i++)
			{
				panels[i].PopulateDisplayables();
			}
		}

		//Array version
		public void SetDisplayableArrayData<T>(T[] sentTArray, int index)
		{
			panels[index].SetData<T>(sentTArray);
		}

		public void SetData<T>()
		{
			if (panels != null)
			{
				for (int i = 0; i < panels.Length; i++)
				{
					panels[i].SetData<T>(null);
				}
			}
		}

		public void Clear()
		{
			for (int i = 0; i < panels.Length; i++)
			{
				panels[i].Clear();
			}
		}

		public virtual void CancelUI()
		{
			if (EndAction == null) { return; }
			EndAction();
		}

		public virtual void Display(bool isVisible)
		{
			if (isVisible)
			{
				Show();
				return;
			}
			Hide();
		}

		protected virtual void Show()
		{
			gameObject.SetActive(true);
			Clear();
		}

		protected virtual void Hide()
		{
			if (panels != null)
			{
				for (int i = 0; i < panels.Length; i++)
				{
					panels[i].DisplayAll();
				}
			}
			gameObject.SetActive(false);
		}
	}
}