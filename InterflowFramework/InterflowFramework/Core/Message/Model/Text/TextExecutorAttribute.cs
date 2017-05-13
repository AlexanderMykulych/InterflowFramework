using InterflowFramework.Core.Message.Model.ExecutorRegistrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Message.Model.Text
{
	public class TextExecutorAttribute : ExecutorAttribute
	{
		public TextExecutorAttribute(string name) : base(name)
		{
		}
	}
}
