using InterflowFramework.Core.Channel.InputPoint;
using InterflowFramework.Core.Factory.PointFactory;
using InterflowFramework.Core.LinqExtension;
using InternalFramework.Nancy.InputPoint;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalFramework.Nancy
{
	public class MainModule: NancyModule
	{
		public MainModule() {
			NancyInputPoint.ForEach(inPoint => InvokeNancyMethod(inPoint.Method, inPoint.Path, inPoint.Action));
		}
		public IEnumerable<NancyInputPoint> NancyInputPoint {
			get {
				return PointFactory.GetInPoints().Where(x => x is NancyInputPoint).Select(x => (NancyInputPoint)x);
			}
		}
		public void InvokeNancyMethod(string method, string path, Func<dynamic, object> action) {
			switch(method) {
				case "Get":
					Get(path, action);
					break;
				case "Put":
					Put(path, action);
					break;
				case "Post":
					Post(path, action);
					break;
				case "Delete":
					Delete(path, action);
					break;
			}
		}
	}
}
