using System;
namespace DSA.Extensions.Base
{
	//used by objects to retrieve data from a monobehaviour
	public interface IReceivable<T, U>
	{
		Func<T, U> ReceiveFunction { get; set; }
	}

	public interface IReceivable<T>
	{
		Func<T> ReceiveFunction { get; set; }
	}
}