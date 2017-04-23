using InterflowFramework.Core.Message.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Message
{
	public interface IMessage
	{
		IMessageRequestResponse ExecuteRequest(IMessageRequest request);
		Object Serealize();
		void Deserealize(Object serealizeObject);
	}
}
