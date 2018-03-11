namespace DSA.Extensions.Base
{
	public interface IDataItem : IOrderable, IUnique
	{
		//Inherits from IOrderable to give the data an ID
		//Provides id'd text data
		string Text { get; }
	}
}