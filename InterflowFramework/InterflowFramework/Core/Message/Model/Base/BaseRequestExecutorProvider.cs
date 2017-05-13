using InterflowFramework.Core.Message.Interface;
using InterflowFramework.Core.Message.Model.ExecutorRegistrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Message.Model.Base
{
	public abstract class BaseRequestExecutorProvider : IRequestExecutorProvider
	{
		public virtual IRequestExecutor Create(string type)
		{
			return Executors.Where(x => x.Key == type).Select(x => x.Value).FirstOrDefault();
		}
		private Dictionary<string, IRequestExecutor> _executors;
		protected virtual Dictionary<string, IRequestExecutor> Executors {
			get {
				if(_executors == null) {
					_executors = typeof(BaseRequestExecutorProvider)
						.Assembly
						.GetTypes()
						.Where(x => x.GetCustomAttributes(GetExecutorAttributeType(), false).Any())
						.Select(x =>
						{
							var attribute = x.GetCustomAttributes(GetExecutorAttributeType(), false).FirstOrDefault();
							if (attribute != null)
							{
								var name = GetExecutorAttributeDataName(attribute);
								var executor = Activator.CreateInstance(x) as IRequestExecutor;
								if (executor != null)
								{
									return new
									{
										name = name,
										executor = executor
									};
								}
							}
							return null;
						})
						.Where(x => x != null)
						.ToDictionary(x => x.name, x => x.executor);
				}
				return _executors;
			}
		}
		protected virtual Type GetExecutorAttributeType() {
			return typeof(ExecutorAttribute);
		}
		protected virtual string GetExecutorAttributeDataName(object attribute) {
			return ((ExecutorAttribute)attribute).Name;
		}
	}
}
