namespace DSA.Extensions.Base
{
	//Used to call methods when an object wakes/spawns/starts without knowing an objects type
	public interface IActivateable
	{
		void Activate();
	}
}