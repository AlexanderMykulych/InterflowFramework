using InterflowFramework.Core.Message.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterflowFramework.Core.Message.Interface;
using InterflowFramework.Core.Message.Model.Json.Executor;
using InterflowFramework.Core.Message.Model.Excell.Executor;

namespace InterflowFramework.Core.Message.Model.Excell
{
	public class ExcellRequestExecutorProvider : BaseRequestExecutorProvider
	{
		protected override Type GetExecutorAttributeType()
		{
			return typeof(ExcellExecutorAttribute);
		}
	}
}
