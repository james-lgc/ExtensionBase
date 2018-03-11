using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DSA.Extensions.Base
{
	public interface IRemoveable
	{
		void Remove();
	}

	public interface IRemoveable<T>
	{
		void Remove(T sentItem);
	}
}