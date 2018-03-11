//used to Process objects/data
//Similair to IProcessor, but intended for sent items that WILL be held in memory after method
//Mostly to avoid confusion due to linguistic distinction between processing and setting
//but also provides flexibility if an object requires both interfaces
namespace DSA.Extensions.Base
{
	public interface ISettable
	{
		void Set();
	}

	public interface ISettable<T>
	{
		void Set(T sentItem);
	}

	public interface ISettable<T, U>
	{
		void Set(T sentItem1, U sentItem2);
	}

	public interface ISettable<T, U, V>
	{
		void Set(T sentItem1, U sentItem2, V sentItem3);
	}

	public interface ISettable<T, U, V, W>
	{
		void Set(T sentItem1, U sentItem2, V sentItem3, W sentItem4);
	}

	public interface ISettable<T, U, V, W, X>
	{
		void Set(T sentItem1, U sentItem2, V sentItem3, W sentItem4, X sentItem5);
	}
}