using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSA.Extensions.Base
{
	public interface IUnique
	{
		string UniqueID { get; }
		void SetUniqueID(IProvider<string, string, string> sentProvider);
	}
}