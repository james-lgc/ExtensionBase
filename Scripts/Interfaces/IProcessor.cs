//used to Process objects/data
//Similair to ISettable, but intended for sent items that WILL NOT be held in memory after method
//Mostly to avoid confusion due to linguistic distinction between processing and setting
//but also provides flexibility if an object requires both interfaces
namespace DSA.Extensions.Base
{
	public interface IProcessor
	{
		void Process();
	}

	public interface IProcessor<T>
	{
		void Process(T sentItem);
	}
}