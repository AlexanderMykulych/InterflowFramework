using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Message.Model.ExecutorRegistrator
{
	[AttributeUsage(AttributeTargets.Class)]
	public class ExecutorAttribute: System.Attribute
	{
		public string Name;
		public ExecutorAttribute(string name) {
			Name = name;
		}
	}
}
