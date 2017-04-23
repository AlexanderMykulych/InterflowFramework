using InterflowFramework.Core.Message.Interface;
using InterflowFramework.Core.Message.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Message.Model.Text
{
	public class TextRequestExecutorProvider: BaseRequestExecutorProvider
	{
		protected static Dictionary<string, IRequestExecutor> executors = new Dictionary<string, IRequestExecutor>()
		{
			{ "regex", new TextRegexRequestExecutor() }
		};
		protected override Dictionary<string, IRequestExecutor> Executors {
			get {
				return executors;
			}
		}
	}
}
