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
		private Func<dynamic, object> _mainAction;
		private Func<dynamic, object> _disabledAction;
		public Func<dynamic, object> Action {
			get {
				if(Enabled) {
					return _mainAction;
				}
				return _disabledAction;
			}
			set {
				_mainAction = value;
			}
		}
		public bool Enabled = false;
		public NancyInputPoint(string method, string path, Func<dynamic, object> action) {
			Method = method;
			Path = path;
			Action = action;
			_disabledAction = _ => HttpStatusCode.NotFound;
		}
		public override void Push(object message)
		{
			throw new NotImplementedException();
		}
		public override void Enable()
		{
			base.Enable();
			Enabled = true;
		}
		public override void Disable()
		{
			base.Disable();
			Enabled = false;
		}
	}
}
