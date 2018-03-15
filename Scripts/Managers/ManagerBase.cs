using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DSA.Extensions.Base
{
	[System.Serializable]
	public abstract class ManagerBase : ExtendedMonoBehaviour
	{
		[SerializeField] bool isInterruptable;
		public bool IsInterruptable { get { return isInterruptable; } }

		private bool isProcessing;
		public bool IsProcessing { get { return isProcessing; } }

		[Header("ManagerLinks")] [SerializeField] private ManagerLinkHandler managerLinkHandler;
		public ManagerLinkHandler ManagerLinkHandler { get { return managerLinkHandler; } }

		public GameObject ThisGO { get { return gameObject; } }

		public delegate void OnProcessEvent();
		public event OnProcessEvent OnStartProcess;
		private Action<ManagerBase> StartAction;
		public event OnProcessEvent OnEndProcess;
		private Action<ManagerBase> EndAction;
		public delegate void OnLoadEvent();
		public event OnLoadEvent OnLoad;
		public delegate void OnLateLoadEvent();
		public event OnLateLoadEvent OnLateLoad;
		public delegate void OnTraitsFoundEvent(TraitedMonoBehaviour sentObj);
		public event OnTraitsFoundEvent OnTraitsFound;

		protected Func<ManagerBase, bool> IsStartableFunction;

		public void SetStartAction(Action<ManagerBase> sentAction) { StartAction = sentAction; }
		public void SetEndAction(Action<ManagerBase> sentAction) { EndAction = sentAction; }
		public void SetIsStartableFunction(Func<ManagerBase, bool> sentFunc) { IsStartableFunction = sentFunc; }

		public virtual void Initialize()
		{
			managerLinkHandler.SetManagerLinks(this);
		}

		public virtual void LateInitialize() { }

		public virtual void PassDelegatesToTraits(TraitedMonoBehaviour sentObj) { }

		public void RaiseTraitsFound(TraitedMonoBehaviour sentObj)
		{
			if (OnTraitsFound == null) { return; }
			OnTraitsFound(sentObj);
		}

		public virtual void Load()
		{
			SetTraits();
		}

		public virtual void LateLoad() { }

		protected void RaiseOnLoadEvent()
		{
			if (OnLoad == null) { return; }
			OnLoad();
			OnLateLoad();
		}

		public virtual void SetTraits() { }

		protected T[] GetTraits<T>() where T : TraitBase
		{
			T[] attributes = FindObjectsOfType<T>();
			return attributes;
		}

		public virtual void QueueProecess()
		{
			if (IsStartableFunction(this))
			{
				StartProcess();
				return;
			}
			StartCoroutine(WaitInQueue());
		}

		protected IEnumerator WaitInQueue()
		{
			while (!IsStartableFunction(this))
			{
				yield return null;
			}
			StartProcess();
		}

		protected virtual void StartProcess()
		{
			StartAction(this);
			isProcessing = true;
			RaiseStartEvent();
			StopAllCoroutines();
		}

		public virtual void EndProcess()
		{
			if (EndAction == null) { return; }
			EndAction(this);
			isProcessing = false;
			RaiseEndEvent();
		}

		protected void RaiseStartEvent()
		{
			if (OnStartProcess != null)
			{
				OnStartProcess();
			}
		}

		protected void RaiseEndEvent()
		{
			if (OnEndProcess != null)
			{
				OnEndProcess();
			}
		}

		public virtual void LoadAtGameStart()
		{
			StopAllCoroutines();
		}

		public virtual void AddDataToArrayList(ArrayList sentArrayList) { }

		public virtual void ProcessArrayList(ArrayList sentArrayList)
		{

		}

		public void LinkSendableToStartProcess(ISendable sendable)
		{
			if (!GetIsExtensionLoaded() || Extension != sendable.Extension) { return; }
			sendable.SendAction = QueueProecess;
		}

		protected void SetTraitActions<T>(TraitedMonoBehaviour sentObj, Action sentAction) where T : TraitBase, ISendable
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

		protected void SetTraitActions<T, U>(TraitedMonoBehaviour sentObj, Action<U> sentAction) where T : TraitBase, ISendable<U>
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

		protected void SetTraitFunctions<T, U>(TraitedMonoBehaviour sentObj, Func<U> sentFunc) where T : TraitBase, IReceivable<U>
		{
			TraitBase[] traits = sentObj.GetArray();
			for (int i = 0; i < traits.Length; i++)
			{
				if (traits[i] is T)
				{
					T newT = (T)traits[i];
					newT.ReceiveFunction = sentFunc;
				}
			}
		}

		protected void SetTraitFunctions<T, U, V>(TraitedMonoBehaviour sentObj, Func<U, V> sentFunc) where T : TraitBase, IReceivable<U, V>
		{
			TraitBase[] traits = sentObj.GetArray();
			for (int i = 0; i < traits.Length; i++)
			{
				if (traits[i] is T)
				{
					T newT = (T)traits[i];
					newT.ReceiveFunction = sentFunc;
				}
			}
		}

		protected void SetDataHolder<T, U>(TraitedMonoBehaviour sentObj, IDataHoldable<U> sentDataHolder) where T : TraitBase, IDataRetrievable<U>
		{
			T[] traits = sentObj.GetArray<T>();
			for (int i = 0; i < traits.Length; i++)
			{
				traits[i].DataHolder = sentDataHolder;
			}
		}

		protected void SetDataHolder<T, U, V>(TraitedMonoBehaviour sentObj, IDataHoldable<U, V> sentDataHolder) where T : TraitBase, IDataRetrievable<U, V>
		{
			T[] traits = sentObj.GetArray<T>();
			for (int i = 0; i < traits.Length; i++)
			{
				traits[i].DataHolder = sentDataHolder;
			}
		}

		public virtual bool ProcessInstruction(InstructionData sentInstruction)
		{
			if (sentInstruction.extension == Extension) { return true; }
			return false;
		}
	}
}