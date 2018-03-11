using System;
using UnityEngine;
using System.Collections.Generic;

namespace DSA.Extensions.Base
{
	[System.Serializable]
	public class PrintableString : DataItem
	{
		//text to write in the Unity Editor
		[TextArea] [SerializeField] private string name;
		public override string Text { get { return name; } }

		private int id;
		//ID used for nesting data in IIndexable objects
		public override int ID { get { return id; } }

		[SerializeField] private string serializedUniqueIDPrefix = "text";
		protected override string uniqueIDPrefix { get { return serializedUniqueIDPrefix; } }

		public PrintableString(string sentText, int sentID = 0)
		{
			name = sentText;
			id = sentID;
		}

		public PrintableString()
		{
			name = "";
			id = 0;
		}

		public override List<string> GetUniqueIDs()
		{
			List<string> tempList = new List<string>();
			tempList.Add(uniqueID);
			return tempList;
		}

		public override void SetUniqueID(IProvider<string, string, string> sentProvider)
		{
			uniqueID = sentProvider.GetItem(uniqueID, uniqueIDPrefix);
		}
	}
}