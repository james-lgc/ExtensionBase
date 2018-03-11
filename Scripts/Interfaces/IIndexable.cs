//Used to nest run-time changeable data
namespace DSA.Extensions.Base
{
	public interface IIndexable
	{
		DataItem GetIndexedData(int[] sentIds, int counter);
	}
}