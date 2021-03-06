﻿using InterflowFramework.Core.Channel.InputPoint.Const;
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
		public bool SetContextInfo;
		private Func<dynamic, NancyModule, object> _mainAction;
		private Func<dynamic, NancyModule, object> _disabledAction;
		public Func<dynamic, NancyModule, object> Action {
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
		public NancyInputPoint(string method, string path, Func<dynamic, NancyModule, object> action = null, bool setContextInfo = true) {
			Method = method;
			Path = path;
			Action = action;
			_disabledAction = (_, context) => HttpStatusCode.NotFound;
			SetContextInfo = setContextInfo;
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
		public virtual object PushWithResponse(dynamic obj, NancyModule context) {
			if (obj is DynamicDictionary) {
				var message = new MessagePackage(((DynamicDictionary)obj).ToDictionary(), GetId());
				IsReturn = false;
				Response = null;
				if (SetContextInfo)
				{
					message.SetAdditionalInfo(context);
				}
				Subscriber.Execute(InputPointEvent.onMessage, message);
				var timeOutTask = Task.Run(() =>
				{
					Thread.Sleep(30000);
				});
				while (!IsReturn && !timeOutTask.IsCompleted) {
				}
				if (Response != null && Response is MessagePackage) {
					message = (MessagePackage)Response;
					return message.Object;
				}
				return HttpStatusCode.RequestTimeout; 
			}
			return HttpStatusCode.NotFound;
		}
		public override void Message(object message)
		{
			Response = message;
			IsReturn = true;
		}
	}
}
