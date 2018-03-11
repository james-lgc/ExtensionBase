using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DSA.Extensions.Base
{
	namespace UISystem
	{
		public class UIElement : MonoBehaviour, IDisplayable, ISettable<DataItem>
		{
			[SerializeField] private UIText textElement;
			[SerializeField] private UIPanel subPanel;

			private int id;
			public int ID { get { return id; } }

			public GameObject ThisGameObject { get { return gameObject; } }

			public void Initialize()
			{
				if (subPanel == null) return;
				subPanel.PopulateDisplayables();
			}

			public void Set(DataItem sentData)
			{
				if (textElement != null)
				{
					textElement.SetText(sentData.Text);
				}
				if (subPanel == null) return;
				if (sentData is INestable<DataItem>)
				{
					INestable<DataItem> returnable = (INestable<DataItem>)sentData;
					subPanel.SetData(returnable.GetArray());
				}
			}

			public void Display(bool isVisible)
			{
				gameObject.SetActive(isVisible);
			}

			public void Clear()
			{
				if (textElement != null)
				{
					textElement.Clear();
				}
				Display(false);
			}

		}
	}
}