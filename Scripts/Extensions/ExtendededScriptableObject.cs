using UnityEngine;
namespace DSA.Extensions.Base
{
	//Base class for scriptable objects which belong to an extension
	public abstract class ExtendedScriptableObject : ScriptableObject, IExtendable
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