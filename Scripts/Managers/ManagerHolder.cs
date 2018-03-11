using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace DSA.Extensions.Base
{
	public class ManagerHolder : MonoBehaviour
	{
		private ManagerBase[] managers;
		private bool isFirstLoad = true;
		private ExtensionEnum extensionEnum;
		private ManagerBase activeManager;

		private void Awake()
		{
			Initialize();
		}

		private void Initialize()
		{
			extensionEnum = new ExtensionEnum();
			if (isFirstLoad == false) return;
			managers = GetComponentsInChildren<ManagerBase>();
			SetActiveExtensions();
			for (int i = 0; i < managers.Length; i++)
			{
				if (!managers[i].GetIsExtensionLoaded()) { continue; }
				for (int j = 0; j < managers.Length; j++)
				{
					managers[i].OnTraitsFound += managers[j].PassDelegatesToTraits;
				}
				managers[i].Initialize();
				managers[i].SetTraits();
				managers[i].LateInitialize();
				managers[i].SetStartAction(SetActiveManager);
				managers[i].SetEndAction(RemoveFromActiveManager);
				managers[i].SetIsStartableFunction(GetIsManagerStartable);
				managers[i].LoadAtGameStart();
			}
			isFirstLoad = false;
		}

		public void SetActiveManager(ManagerBase sentManager)
		{
			bool isSettable = true;
			if (activeManager != null)
			{
				if (activeManager.ManagerLinkHandler.SynchronousManagers.Contains(sentManager)) { isSettable = false; }
				if (activeManager.ManagerLinkHandler.ParallelManagers.Contains(sentManager)) { isSettable = false; }
				if (!(sentManager == activeManager)) { isSettable = false; }
			}
			if (isSettable == true) { activeManager = sentManager; }
		}

		public bool GetIsManagerStartable(ManagerBase sentManager)
		{
			bool isStartable = false;
			if (activeManager == null) return true;
			else if (activeManager.ManagerLinkHandler.SynchronousManagers.Contains(sentManager)) { isStartable = true; }
			else if (activeManager.ManagerLinkHandler.ParallelManagers.Contains(sentManager)) { isStartable = true; }
			else if (sentManager.ManagerLinkHandler.ParallelManagers.Contains(activeManager)) { isStartable = true; }
			else if (sentManager.ManagerLinkHandler.StartEndLinkedManagers.Contains(activeManager)) { isStartable = true; }
			else if (activeManager == sentManager) { isStartable = true; }
			else if (activeManager.IsInterruptable) { isStartable = true; }
			return isStartable;
		}

		public void RemoveFromActiveManager(ManagerBase sentManager)
		{
			if (activeManager == sentManager) { activeManager = null; }
		}

		private void SetActiveExtensions()
		{
			foreach (ExtensionEnum.Extension extension in System.Enum.GetValues(typeof(ExtensionEnum.Extension)))
			{
				ManagerBase manager = null;
				ExtensionEnum.ActiveExtensionDict.Add(extension, GetIsExtensionActive(extension));
				for (int i = 0; i < managers.Length; i++)
				{
					if (managers[i].Extension == extension)
					{
						manager = managers[i];
						break;
					}
				}
			}
		}

		private bool GetIsExtensionActive(ExtensionEnum.Extension sentExtension)
		{
			for (int i = 0; i < managers.Length; i++)
			{
				if (managers[i].Extension == sentExtension)
				{
					if (managers[i].ThisGO.activeSelf == false) return false;
					return true;
				}
			}
			return false;
		}
	}
}