using InterflowFramework.Core.Channel.Identified;
using InterflowFramework.Core.Message.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Message
{
	public interface IMessage: IIdentified
	{
		IMessageRequestResponse ExecuteRequest(IMessageRequest request);
		Object Serealize();
		void Deserealize(Object serealizeObject);
	}
}
