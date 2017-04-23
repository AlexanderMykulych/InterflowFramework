using InterflowFramework.Core.Message.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Message.Model.Base
{
	public abstract class BaseRequestExecutor : IRequestExecutor
	{
		protected abstract IMessageRequestResponse CreateResponse();
		public abstract IEnumerable<object> GetMathes(string request, IMessage requestedObject);

		public IMessageRequestResponse Execute(string request, IMessage requestedObject)
		{
			IMessageRequestResponse response = CreateResponse();
			response.SetLazy(() => GetMathes(request, requestedObject));
			return response;
		}
	}
}
