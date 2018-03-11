using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DSA.Extensions.Base
{
	[System.Serializable]
	public class ManagerLinkHandler
	{
		[SerializeField] private ManagerBase[] startEndLinkedManagers;
		[SerializeField] private ManagerBase[] endStartLinkedManagers;
		[SerializeField] private ManagerBase[] loadLinkedManagers;
		[SerializeField] private ManagerBase[] synchronousManagers;
		[SerializeField] private ManagerBase[] parallelManagers;
		public ManagerBase[] SynchronousManagers { get { return synchronousManagers; } }
		public ManagerBase[] ParallelManagers { get { return parallelManagers; } }
		public ManagerBase[] StartEndLinkedManagers { get { return startEndLinkedManagers; } }

		public void SetManagerLinks(ManagerBase sentManager)
		{
			for (int i = 0; i < synchronousManagers.Length; i++)
			{
				if (!sentManager.GetIsExtensionLoaded()) { break; }
				if (!synchronousManagers[i].GetIsExtensionLoaded()) { continue; }
				sentManager.OnStartProcess += synchronousManagers[i].QueueProecess;
				sentManager.OnEndProcess += synchronousManagers[i].EndProcess;
			}
			for (int i = 0; i < parallelManagers.Length; i++)
			{
				if (!sentManager.GetIsExtensionLoaded()) { break; }
				if (!parallelManagers[i].GetIsExtensionLoaded()) { continue; }
				sentManager.OnEndProcess += parallelManagers[i].EndProcess;
			}
			for (int i = 0; i < startEndLinkedManagers.Length; i++)
			{
				if (!sentManager.GetIsExtensionLoaded()) { break; }
				if (!startEndLinkedManagers[i].GetIsExtensionLoaded()) { continue; }
				sentManager.OnStartProcess += startEndLinkedManagers[i].EndProcess;
			}
			for (int i = 0; i < endStartLinkedManagers.Length; i++)
			{
				if (!sentManager.GetIsExtensionLoaded()) { break; }
				if (!endStartLinkedManagers[i].GetIsExtensionLoaded()) { continue; }
				sentManager.OnEndProcess += endStartLinkedManagers[i].QueueProecess;
			}
			for (int i = 0; i < loadLinkedManagers.Length; i++)
			{
				if (!sentManager.GetIsExtensionLoaded()) { break; }
				if (!loadLinkedManagers[i].GetIsExtensionLoaded()) { continue; }
				sentManager.OnLoad += loadLinkedManagers[i].Load;
				sentManager.OnLateLoad += loadLinkedManagers[i].LateLoad;
			}
		}
	}
}