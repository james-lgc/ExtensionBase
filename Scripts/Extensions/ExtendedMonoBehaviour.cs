using UnityEngine;

namespace DSA.Extensions.Base
{
	[System.Serializable]
	//Base class for all monobheaviours which belong to an extension
	public abstract class ExtendedMonoBehaviour : MonoBehaviour, IExtendable
	{
		//Which extension the monobehaviour belongs to
		public abstract ExtensionEnum.Extension Extension { get; }

		//Checks if the extension is Loaded against a static dictionary
		public virtual bool GetIsExtensionLoaded()
		{
			bool isActive = false;
			ExtensionEnum.ActiveExtensionDict.TryGetValue(Extension, out isActive);
			return isActive;
		}
	}
}