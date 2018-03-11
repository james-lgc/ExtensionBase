using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DSA.Extensions.Base
{
	[System.Serializable]
	public class UIManager : ManagerBase
	{
		public override ExtensionEnum.Extension Extension { get { return ExtensionEnum.Extension.UI; } }

		[SerializeField] private UICanvas[] canvases;
		private UIController uiController;
		private UICanvas activeCanvas;

		[Header("Colour Scheme")] [SerializeField] private Color panelColour;
		public Color PanelColour { get { return panelColour; } }
		[SerializeField] private Color buttonImageColor;
		public Color ButtonImageColor { get { return buttonImageColor; } }
		[SerializeField] private Color textColour;
		public Color TextColour { get { return textColour; } }
		[SerializeField] private ColorBlock buttonColours;
		public ColorBlock ButtonColours { get { return buttonColours; } }
		[SerializeField] private Font textFont;
		public Font TextFont { get { return textFont; } }

		[Header("UI Elements")] [SerializeField] private Image[] panels;
		public Image[] Panels { get { return panels; } }
		[SerializeField] private Text[] texts;
		public Text[] Texts { get { return texts; } }
		[SerializeField] private Button[] buttons;
		public Button[] Buttons { get { return buttons; } }

		public override void Initialize()
		{
			base.Initialize();
			uiController = GetComponent<UIController>();
		}

		public override void PassDelegatesToTraits(TraitedMonoBehaviour sentObj)
		{
			SetTraitActions<UICanvasTrait, UICanvas>(sentObj, SetActiveCanvas);
		}

		public void SetActiveCanvas(UICanvas sentCanvas)
		{
			activeCanvas = sentCanvas;
		}

		protected override void StartProcess()
		{
			base.StartProcess();
			uiController.enabled = true;
		}

		public override void EndProcess()
		{
			base.EndProcess();
			activeCanvas = null;
			uiController.enabled = false;
		}

		public void CallCanvasCancel()
		{
			if (activeCanvas == null) { return; }
			activeCanvas.CancelUI();
		}

		[ExecuteInEditMode]
		public void UpdatePanelColours()
		{
			foreach (Image panel in panels)
			{
				panel.color = panelColour;
			}
		}
		[ExecuteInEditMode]
		public void UpdateTextColours()
		{
			foreach (Text text in texts)
			{
				text.color = textColour;
			}
		}
		[ExecuteInEditMode]
		public void UpdateButtonColours()
		{
			foreach (Button button in buttons)
			{
				button.GetComponent<Image>().color = buttonImageColor;
				button.colors = buttonColours;
			}
		}
		[ExecuteInEditMode]
		public void UpdateTextFonts()
		{
			foreach (Text text in texts)
			{
				text.font = textFont;
			}
		}

		public void SetUIElements()
		{
			panels = GetTArray<Image>();
			texts = GetTArray<Text>();
			buttons = GetTArray<Button>();
		}

		private T[] GetTArray<T>()
		{
			T[] newTArray;
			int arrayLength = 0;
			for (int i = 0; i < canvases.Length; i++)
			{
				T[] tempArray = canvases[i].gameObject.GetComponentsInChildren<T>();
				arrayLength = arrayLength + tempArray.Length;
			}
			newTArray = new T[arrayLength];
			int currentCount = 0;
			for (int i = 0; i < canvases.Length; i++)
			{
				T[] tempArray = canvases[i].gameObject.GetComponentsInChildren<T>();
				for (int j = 0; j < tempArray.Length; j++)
				{
					newTArray[j + currentCount] = tempArray[j];
				}
				currentCount = currentCount + tempArray.Length;
			}
			return newTArray;
		}
	}
}