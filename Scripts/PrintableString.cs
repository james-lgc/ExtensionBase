using System;
using UnityEngine;
using System.Collections.Generic;

namespace DSA.Extensions.Base
{
	[System.Serializable]
	public class PrintableString : DataItem
	{
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

		public override string GetLabelText()
		{
			return name;
		}

		public override string GetEndLabelText()
		{
			return null;
		}

		public override void SetAsNew()
		{
			name = "New PrintableString";
			uniqueID = null;
			id = 0;
		}
	}
}