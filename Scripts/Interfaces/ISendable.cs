using System;
namespace DSA.Extensions.Base
{
	//used by objects to send data or trigger a method
	public interface ISendable : IExtendable
	{
		Action SendAction { get; set; }
	}

	public interface ISendable<T> : IExtendable
	{
		Action<T> SendAction { get; set; }
	}

	public interface ISendable<T, U> : IExtendable
	{
		Action<T, U> SendAction { get; set; }
	}
}