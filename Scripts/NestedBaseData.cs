using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace DSA.Extensions.Base
{
	[System.Serializable]
	//The base class for serializable nested data that may be altered at run time
	public abstract class NestedBaseData<T> : DataItem, IIndexable, INestable<DataItem> where T : DataItem
	{
		//array of nested DataItems
		[SerializeField] protected T[] dataArray;

		//create a new instance containing an array of the nested type
		public NestedBaseData(T[] sentArray)
		{
			SetArray(sentArray);
		}

		//create a new instance containing an 1 long array of the nested type
		public NestedBaseData(T sentData)
		{
			T[] tempDataArray = new T[1];
			tempDataArray[0] = sentData;
			SetArray(tempDataArray);
		}

		public NestedBaseData() : base() { }

		//used to return the nested data
		public virtual DataItem[] GetArray()
		{
			return dataArray;
		}

		//used to set the nested data
		protected virtual void SetArray(T[] sentData)
		{
			dataArray = sentData;
		}

		//returns nested indexed data from provided ids
		//counter counts how many levels of nesting have been reached
		public DataItem GetIndexedData(int[] sentIds, int counter = 0)
		{
			DataItem[] tempDataArray = GetArray();
			//if the desired nested depth has been reached, return this item
			if (counter >= sentIds.Length) return this;
			//otherwise get an item from the array
			for (int i = 0; i < tempDataArray.Length; i++)
			{
				//check if this array item's data matches the sent index
				if (tempDataArray[i].ID == sentIds[counter])
				{
					if (tempDataArray[i] is IIndexable)
					{
						IIndexable indexable = (IIndexable)tempDataArray[i];
						//continue to retrieve data from indexables until desired depth is reached
						DataItem dataItem = indexable.GetIndexedData(sentIds, counter + 1);
						return dataItem;
					}
				}
			}
			return null;
		}

		public T GetArrayItem(int sentID)
		{
			return (T)GetArray()[sentID];
		}

		protected List<string> GetChildUniqueIDs(DataItem[] sentData)
		{
			List<string> childIDs = GetNewStringList();
			for (int i = 0; i < sentData.Length; i++)
			{
				childIDs = childIDs.Concat(sentData[i].GetUniqueIDs()).ToList();
			}
			return childIDs;
		}

		protected List<string> GetChildUniqueIDs<U>(List<U> sentData) where U : DataItem
		{
			List<string> childIDs = GetNewStringList();
			for (int i = 0; i < sentData.Count; i++)
			{
				childIDs = childIDs.Concat(sentData[i].GetUniqueIDs()).ToList();
			}
			return childIDs;
		}

		protected void SetChildUnqueIDs(IProvider<string, string, string> sentProvider)
		{
			if (dataArray == null) { return; }
			SetArrayUniqueIDs(dataArray, sentProvider);
		}

		protected void SetArrayUniqueIDs(DataItem[] sentArray, IProvider<string, string, string> sentProvider)
		{
			for (int i = 0; i < sentArray.Length; i++)
			{
				sentArray[i].SetUniqueID(sentProvider);
			}
		}
	}
}