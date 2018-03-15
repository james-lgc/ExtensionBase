using System.Collections.Generic;
using UnityEngine;
namespace DSA.Extensions.Base
{
	//Contains a value for each managed extension
	public enum ExtensionEnum { None, GamePlay, Scene, Story, Conversation, Exit, Character, Menu, UI, Time, Player, Save, Load, Options, Audio }
	[SerializeField]
	public class ExtensionEnumHolder
	{
		private static Dictionary<ExtensionEnum, bool> activeExtensionDict;
		//used to keep track of currently loaded extensions
		public static Dictionary<ExtensionEnum, bool> ActiveExtensionDict
		{
			get
			{
				if (activeExtensionDict == null) activeExtensionDict = new Dictionary<ExtensionEnum, bool>();
				return activeExtensionDict;
			}
		}
	}
}