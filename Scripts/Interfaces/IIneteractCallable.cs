//method called by IInteractable objects
//As IInteractable is called through physics ray cast hits,
//this ensures the main interactable base is called first
namespace DSA.Extensions.Base
{
	public interface IInteractCallable
	{
		void CallInteract();
	}
}