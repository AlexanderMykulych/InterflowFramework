using InterflowFramework.Core.Message.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterflowFramework.Core.Message.Interface;
using InterflowFramework.Core.Message.Model.Json.Executor;

namespace InterflowFramework.Core.Message.Model.Json
{
	public class JsonRequestExecutorProvider : BaseRequestExecutorProvider
	{
		protected override Type GetExecutorAttributeType()
		{
			return typeof(JsonExecutorAttribute);
		}
	}
}
