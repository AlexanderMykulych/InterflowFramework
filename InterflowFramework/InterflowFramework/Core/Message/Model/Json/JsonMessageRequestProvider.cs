using InterflowFramework.Core.Message.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterflowFramework.Core.Message.Interface;

namespace InterflowFramework.Core.Message.Model.Json
{
	public class JsonMessageRequestProvider : BaseMessageRequestProvider
	{
		protected override IMessageRequest CreateRequest()
		{
			return new JsonMessageRequest();
		}
	}
}
