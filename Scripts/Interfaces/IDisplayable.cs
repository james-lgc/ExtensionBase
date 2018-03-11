using UnityEngine;
namespace DSA.Extensions.Base
{
	//Interface for displayable UI Items, displaying one data item.  Displays IPrintable Text
	public interface IDisplayable : IInitializeable
	{
		void Display(bool isVisible);

		void Clear();
		//used to provide gameobject to unitys selection handler from interface
		GameObject ThisGameObject { get; }
	}
}