using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace DSA.Extensions.Base
{
	public class ManagerHolder : ExtendedMonoBehaviour
	{
		private ManagerBase[] managers;
		private bool isFirstLoad = true;
		private ExtensionEnum extensionEnum;
		private ManagerBase activeManager;

		public override ExtensionEnum Extension { get { return ExtensionEnum.None; } }

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
				managers[i].OnTraitsFound += PassDelegatesToTraits;
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
			foreach (ExtensionEnum extension in System.Enum.GetValues(typeof(ExtensionEnum)))
			{
				ManagerBase manager = null;
				ExtensionEnumHolder.ActiveExtensionDict.Add(extension, GetIsExtensionActive(extension));
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

		private bool GetIsExtensionActive(ExtensionEnum sentExtension)
		{
			if (sentExtension == ExtensionEnum.None) { return true; }
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

		private void PassDelegatesToTraits(TraitedMonoBehaviour sentObj)
		{
			SetTraitActions<InstructionTrait, InstructionData[]>(sentObj, PassInstructionToManagers);
		}

		private void SetTraitActions<T, U>(TraitedMonoBehaviour sentObj, Action<U> sentAction) where T : TraitBase, ISendable<U>
		{
			TraitBase[] traits = sentObj.GetArray();
			for (int i = 0; i < traits.Length; i++)
			{
				if (traits[i] is T)
				{
					T newT = (T)traits[i];
					newT.SendAction = sentAction;
				}
			}
		}

		private void PassInstructionToManagers(InstructionData[] sentInstructions)
		{
			if (sentInstructions == null) { return; }
			for (int i = 0; i < managers.Length; i++)
			{
				for (int j = 0; j < sentInstructions.Length; j++)
				{
					if (managers[i].ProcessInstruction(sentInstructions[j]))
					{
						break;
					}
				}
			}
		}
	}
}