using InterflowFramework.Core.Message.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Message.Model.Base
{
	public abstract class BaseMessageRequest : IMessageRequest
	{
		protected string Request;
		protected string RequestType;
		protected string RequestValue;
		protected abstract IRequestExecutorProvider ExecutorProvider { get; }
		public virtual IMessageRequestResponse Execute(IMessage message)
		{
			var executor = ExecutorProvider.Create(RequestType);
			if (executor != null)
			{
				return Execute(executor, message);
			}
			return null;
		}
		protected virtual IMessageRequestResponse Execute(IRequestExecutor executor, IMessage message)
		{
			return executor.Execute(RequestValue, message);
		}
		public void SetRequestText(string request)
		{
			Request = request;
			ParseRequest();
		}
		protected virtual void ParseRequest()
		{
			int splitIndex = Request.IndexOf(":");
			if (splitIndex > -1)
			{
				RequestType = Request.Substring(0, splitIndex);
				RequestValue = Request.Substring(splitIndex + 1);
			} else {
				RequestType = Request;
				RequestValue = null;
			}
		}
	}
}
