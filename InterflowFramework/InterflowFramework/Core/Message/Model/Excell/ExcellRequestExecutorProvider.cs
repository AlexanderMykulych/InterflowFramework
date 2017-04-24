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
		protected override Dictionary<string, IRequestExecutor> Executors {
			get {
				return executors;
			}
		}
		protected static Dictionary<string, IRequestExecutor> executors = new Dictionary<string, IRequestExecutor>() {
			{ "excel", new ExcellRequestExecutor() }
		};
	}
}
