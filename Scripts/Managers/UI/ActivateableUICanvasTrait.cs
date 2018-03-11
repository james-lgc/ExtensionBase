using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DSA.Extensions.Base
{
	[AddComponentMenu("Trait/UI/(Activateable) UI Canvas Trait")]
	[System.Serializable]
	public class ActivateableUICanvasTrait : UICanvasTrait, IActivateable
	{
		public void Activate()
		{
			Use();
		}
	}
}