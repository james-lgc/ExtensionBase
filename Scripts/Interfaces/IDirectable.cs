using System;
namespace DSA.Extensions.Base
{
	//Similair to ISendable, but defined as specific to objects that pass themselves as references
	public interface IDirectable<T> where T : IDirectable<T>
	{
		Action<T> DirectAction { get; set; }
	}
}