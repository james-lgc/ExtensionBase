namespace DSA.Extensions.Base
{//provides a data holder to an object
	public interface IDataRetrievable<T>
	{
		IDataHoldable<T> DataHolder { set; }
	}

	public interface IDataRetrievable<T, U>
	{
		IDataHoldable<T, U> DataHolder { set; }
	}
}