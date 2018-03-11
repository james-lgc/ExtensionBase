using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DSA.Extensions.Base
{
	public class UIText : Text, ISettable<DataItem>, IDisplayable
	{
		[SerializeField] private string preText;
		public string PreText { get { return preText; } set { preText = value; } }
		[SerializeField] private string postText;
		public string PostText { get { return postText; } set { postText = value; } }

		private int id;
		public int ID { get { return id; } }

		public GameObject ThisGameObject { get { return gameObject; } }

		public void Initialize()
		{
			Clear();
		}

		public void SetAdditionalTexts(string sentPre, string sentPost)
		{
			preText = sentPre;
			postText = sentPost;
		}

		public void SetText(string sentText)
		{
			text = preText + sentText + postText;
		}

		public void Clear()
		{
			text = "";
		}

		public void Set(DataItem sentItem)
		{
			SetText(sentItem.Text);
		}

		public void Display(bool isVisible)
		{
			gameObject.SetActive(isVisible);
		}
	}
}