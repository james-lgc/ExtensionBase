using System;
using System.Collections.Generic;
using System.Linq;

namespace DSA.Extensions.Base
{
	[System.Serializable]
	//Base class for monobehaviours which posses traits
	public abstract class TraitedMonoBehaviour : ExtendedMonoBehaviour, IGernericallyNestable<TraitBase>, ISequenceable, IActivateable
	{
		private TraitBase[] traits;

		//returns all of the objects traits
		public TraitBase[] GetArray()
		{
			if (traits == null)
			{
				traits = GetComponents<TraitBase>();
			}
			return traits;
		}

		//returns traits of a given type or interface
		public T[] GetArray<T>() where T : TraitBase
		{
			TraitBase[] tempTraits = GetArray();
			List<T> tempList = new List<T>();
			for (int i = 0; i < tempTraits.Length; i++)
			{
				if (tempTraits[i] is T)
				{
					T newT = (T)tempTraits[i];
					tempList.Add(newT);
				}
			}
			T[] newTraits = tempList.ToArray();
			return newTraits;
		}

		public ISendable<T>[] GetSendableArray<T>()
		{
			TraitBase[] tempTraits = GetArray();
			List<ISendable<T>> tempList = new List<ISendable<T>>();
			for (int i = 0; i < tempTraits.Length; i++)
			{
				if (tempTraits[i] is ISendable<T>)
				{
					ISendable<T> newT = (ISendable<T>)tempTraits[i];
					tempList.Add(newT);
				}
			}
			ISendable<T>[] newTraits = tempList.ToArray();
			return newTraits;
		}

		public IReceivable<T>[] GetReceivableArray<T>()
		{
			TraitBase[] tempTraits = GetArray();
			List<IReceivable<T>> tempList = new List<IReceivable<T>>();
			for (int i = 0; i < tempTraits.Length; i++)
			{
				if (tempTraits[i] is IReceivable<T>)
				{
					IReceivable<T> newT = (IReceivable<T>)tempTraits[i];
					tempList.Add(newT);
				}
			}
			IReceivable<T>[] newTraits = tempList.ToArray();
			return newTraits;
		}

		//calls sequenceable traits that have a matching index
		public void CallInSequence(int order)
		{
			TraitBase[] tempTraits = GetArray();
			for (int i = 0; i < tempTraits.Length; i++)
			{
				if (tempTraits[i] is ISequenceable)
				{
					ISequenceable sequenceable = (ISequenceable)tempTraits[i];
					sequenceable.CallInSequence(order);
				}
			}
		}

		//calls the activate method in any activateable traits
		public void Activate()
		{
			TraitBase[] tempTraits = GetArray();
			for (int i = 0; i < tempTraits.Length; i++)
			{
				if (tempTraits[i] is IActivateable)
				{
					IActivateable activateable = (IActivateable)tempTraits[i];
					activateable.Activate();
				}
			}
		}
	}
}