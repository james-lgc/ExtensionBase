//used to provide an array of a given type
namespace DSA.Extensions.Base
{
	public interface INestable<T>
	{
		T[] GetArray();
	}

	public interface IGernericallyNestable<T> : INestable<T>
	{
		U[] GetArray<U>() where U : T;
	}
}