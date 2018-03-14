using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSA.Extensions.Base
{
	public abstract class DataItem : IUnique, IDataItem
	{
		[SerializeField] protected string uniqueID;
		public string UniqueID { get { return uniqueID; } }
		protected abstract string uniqueIDPrefix { get; }

		protected System.Func<string, string, string> getUniqueIDFunc;
		//The name or description of the data
		[SerializeField] protected string name;
		public virtual string Text { get { return name; } }

		[SerializeField] protected int id;
		public virtual int ID { get { return id; } }

		[SerializeField] protected bool isExpanded;

		public virtual string GetLabelText()
		{
			return Text;
		}

		public abstract string GetEndLabelText();

		public abstract void SetAsNew();

		public abstract void SetUniqueID(IProvider<string, string, string> sentProvider);

		public abstract List<string> GetUniqueIDs();

		protected List<string> GetNewStringList()
		{
			return new List<string>();
		}
	}
}