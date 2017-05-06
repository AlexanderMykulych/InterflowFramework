using InterflowFramework.Core.Channel.InputPoint.Model;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalFramework.Nancy.InputPoint
{
	public class NancyInputPoint: BaseInputPoint
	{
		public string Method;
		public string Path;
		public Func<dynamic, object> Action;
		public NancyInputPoint(string method, string path, Func<dynamic, object> action) {
			Method = method;
			Path = path;
			Action = action;
		}
		public override void Push(object message)
		{
			throw new NotImplementedException();
		}
	}
}
