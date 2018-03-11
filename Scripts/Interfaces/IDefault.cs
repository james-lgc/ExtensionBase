using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DSA.Extensions.Base
{
	public interface IDefault
	{
		void SetDefault();
	}

	public interface IDefault<T>
	{
		void SetDefault(T sentItem);
	}

	public interface IDefault<T, U>
	{
		void SetDefault(T sentItem1, U sentItem2);
	}
}