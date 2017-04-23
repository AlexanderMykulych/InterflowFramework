using InterflowFramework.Core.Message.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Message.Model.Base
{
	public abstract class BaseMessageRequestProvider : IMessageRequestProvider
	{
		public virtual IMessageRequest CreateRequest(string request)
		{
			IMessageRequest messageRequest = CreateRequest();
			messageRequest.SetRequestText(request);
			return messageRequest;
		}
		protected abstract IMessageRequest CreateRequest();
	}
}
