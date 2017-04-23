using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Message.Interface
{
	public interface IMessageRequestProvider
	{
		IMessageRequest CreateRequest(string request);
	}
}
