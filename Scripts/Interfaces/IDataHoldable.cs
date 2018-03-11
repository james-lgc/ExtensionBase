using System;
namespace DSA.Extensions.Base
{
	//Interface for an Intermediary between high and low level classes and seperate extensions
	//Designed to receive data from a manager, then combine with other data holders to return final data

	//single genereic parametered version
	public interface IDataHoldable<T>
	{
		//no public getter.  This prevents getting data from the func directly
		//data should always be retrieved by GetData method
		//this allows linking with other dataholders to provide final data
		Func<T> GetDataFunc { set; }

		T GetData();
	}

	//two generic parametered version
	public interface IDataHoldable<T, U>
	{
		Func<T, U> GetDataFunc { set; }

		U GetData(T sentInfo);
	}
}