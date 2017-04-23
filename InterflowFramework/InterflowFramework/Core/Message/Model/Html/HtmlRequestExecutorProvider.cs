using System;
using System.Collections.Generic;
using InterflowFramework.Core.Message.Interface;
using InterflowFramework.Core.Message.Model.Base;
using InterflowFramework.Core.Message.Model.Html.Executor;

namespace InterflowFramework.Core.Message.Model.Html
{
	internal class HtmlRequestExecutorProvider : BaseRequestExecutorProvider
	{
		protected override Dictionary<string, IRequestExecutor> Executors {
			get {
				return executors;
			}
		}
		protected static Dictionary<string, IRequestExecutor> executors = new Dictionary<string, IRequestExecutor>() {
			{ "xpath", new HtmlXPathRequestExecutor() }
		};
	}
}