using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace DSA.Extensions.Base
{
	public abstract class UIButtonBase : Button, IExtendable, IDisplayable
	{
		public abstract ExtensionEnum.Extension Extension { get; }

		public bool GetIsExtensionLoaded()
		{
			bool isActive = false;
			ExtensionEnum.ActiveExtensionDict.TryGetValue(Extension, out isActive);
			return isActive;
		}

		[SerializeField] protected string preText;
		public string PreText { get { return preText; } set { preText = value; } }
		[SerializeField] protected string postText;
		public string PostText { get { return postText; } set { postText = value; } }

		[SerializeField] private int id;
		public int ID { get { return id; } }

		[SerializeField] private Text textBox;
		public Text TextBox { get { if (textBox == null) textBox = GetComponentInChildren<Text>(); return textBox; } }

		public GameObject ThisGameObject { get { return this.gameObject; } }

		protected abstract void Use();

		public virtual void Initialize()
		{
			base.onClick.AddListener(Use);
			if (textBox != null) return;
			textBox = GetComponentInChildren<Text>();
		}

		public virtual void Clear()
		{
			if (TextBox != null)
			{
				TextBox.text = "";
			}
			Display(false);
		}

		public void Display(bool isVisible)
		{
			gameObject.SetActive(isVisible);
		}

		public void SetText(string sentText)
		{
			if (TextBox == null) return;
			TextBox.text = TextBox.text = preText + sentText + postText;
		}
	}
}