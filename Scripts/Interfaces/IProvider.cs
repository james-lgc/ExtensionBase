//Used to provide an instance of a given type
namespace DSA.Extensions.Base
{
	public interface IProvider<T>
	{
		T GetItem();
	}

	public interface IProvider<T, U>
	{
		T GetItem(U sentItem);
	}

	public interface IProvider<T, U, V>
	{
		T GetItem(U sentItem1, V sentItem2);
	}
}