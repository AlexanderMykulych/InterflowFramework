using InterflowFramework.Core.Message.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterflowFramework.Core.Message.Interface;
using InterflowFramework.Core.Message.Model.Xml.Executor;

namespace InterflowFramework.Core.Message.Model.Xml
{
	public class XmlRequestExecutorProvider : BaseRequestExecutorProvider
	{
		protected override Type GetExecutorAttributeType()
		{
			return typeof(XmlExecutorAttribute);
		}
	}
}
