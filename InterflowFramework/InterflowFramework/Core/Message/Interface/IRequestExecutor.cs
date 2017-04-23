namespace InterflowFramework.Core.Message.Interface
{
	public interface IRequestExecutor
	{
		IMessageRequestResponse Execute(string request, IMessage requestedObject);
	}
}