//used by objects to call anonymous functions in sequenced stages.
namespace DSA.Extensions.Base
{
	public interface ISequenceable
	{
		void CallInSequence(int sequenceID);
	}
}