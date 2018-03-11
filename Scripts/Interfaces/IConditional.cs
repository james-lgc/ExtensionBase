//used to check if a condition is met without knowing object type
namespace DSA.Extensions.Base
{
	public interface IConditional
	{
		bool GetIsConditionMet();
		void MeetCondition();
	}
}