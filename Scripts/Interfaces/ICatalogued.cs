using System.Collections.Generic;
namespace DSA.Extensions.Base
{
	//Provides a Dictionary of items
	public interface ICatalogued<T, U>
	{
		Dictionary<T, U> GetDictionary();
	}
}