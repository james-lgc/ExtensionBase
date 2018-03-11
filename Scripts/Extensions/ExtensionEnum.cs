using System.Collections.Generic;
using UnityEngine;
namespace DSA.Extensions.Base
{
	[SerializeField]
	public class ExtensionEnum
	{
		//Contains a value for each managed extension
		public enum Extension { None, GamePlay, Scene, Story, Conversation, Exit, Character, Menu, UI, Time, Player, Save, Load, Options, Audio }

		private static Dictionary<Extension, bool> activeExtensionDict;
		//used to keep track of currently loaded extensions
		public static Dictionary<Extension, bool> ActiveExtensionDict
		{
			get
			{
				if (activeExtensionDict == null) activeExtensionDict = new Dictionary<Extension, bool>();
				return activeExtensionDict;
			}
		}
	}
}