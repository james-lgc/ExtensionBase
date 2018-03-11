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
		public abstract string Text { get; }

		public abstract int ID { get; }

		public DataItem()
		{

		}

		public abstract void SetUniqueID(IProvider<string, string, string> sentProvider);

		public abstract List<string> GetUniqueIDs();

		protected List<string> GetNewStringList()
		{
			return new List<string>();
		}
	}
}