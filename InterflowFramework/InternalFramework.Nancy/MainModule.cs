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
		public void InvokeNancyMethod(string method, string path, Func<dynamic, NancyModule, object> action) {
			Func<dynamic, object> baseAction = obj => action(obj, this);
			switch(method) {
				case "Get":
					Get(path, baseAction);
					break;
				case "Put":
					Put(path, baseAction);
					break;
				case "Post":
					Post(path, baseAction);
					break;
				case "Delete":
					Delete(path, baseAction);
					break;
			}
		}
	}
}
