using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace DSA.Extensions.Base
{
	//Ensure the trait base can only be attached to a traited object
	[RequireComponent(typeof(TraitedMonoBehaviour))]
	[System.Serializable]
	//Base class for behaviour traits
	public abstract class TraitBase : ExtendedMonoBehaviour
	{

	}
}