//links an object to an Extension (Scene, Conversation, etc).
namespace DSA.Extensions.Base
{
	public interface IExtendable
	{
		ExtensionEnum.Extension Extension { get; }
	}
}