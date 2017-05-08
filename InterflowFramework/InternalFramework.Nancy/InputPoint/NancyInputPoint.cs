using InterflowFramework.Core.Channel.InputPoint.Const;
using InterflowFramework.Core.Channel.InputPoint.Model;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InternalFramework.Nancy.InputPoint
{
	public class NancyInputPoint : BaseInputPoint
	{
		public string Method;
		public string Path;
		public bool IsReturn;
		public object Response;
		private Func<dynamic, object> _mainAction;
		private Func<dynamic, object> _disabledAction;
		public Func<dynamic, object> Action {
			get {
				if (Enabled) {
					if (_mainAction == null) {
						return PushWithResponse;
					}
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
		public virtual object PushWithResponse(dynamic obj) {
			var message = new MessagePackage(obj, GetId());
			IsReturn = false;
			Response = null;
			
			Subscriber.Execute(InputPointEvent.onMessage, message);
			var timeOutTask = Task.Run(() =>
			{
				Thread.Sleep(30000);
			});
			while(!IsReturn && !timeOutTask.IsCompleted) {
			}
			if(Response != null && Response is MessagePackage) {
				message = (MessagePackage)Response;
				return message.Object;
			}
			return HttpStatusCode.RequestTimeout;
		}
		public override void Message(object message)
		{
			Response = message;
			IsReturn = true;
		}
	}
}
