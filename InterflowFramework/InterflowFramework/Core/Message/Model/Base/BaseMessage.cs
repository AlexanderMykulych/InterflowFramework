﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterflowFramework.Core.Message.Interface;

namespace InterflowFramework.Core.Message.Model.Base
{
	public abstract class BaseMessage : IMessage
	{
		private string _id;
		public string Id {
			get {
				if(string.IsNullOrEmpty(_id)) {
					_id = Guid.NewGuid().ToString();
				}
				return _id;
			}
			set {
				_id = value;
			}
		}
		public virtual void Deserealize(object serealizeObject)
		{
			throw new NotImplementedException();
		}
		public virtual object Serealize()
		{
			throw new NotImplementedException();
		}
		public virtual IMessageRequestResponse ExecuteRequest(IMessageRequest request) {
			if (ValidateRequest(request))
			{
				return request.Execute(this);
			}
			else
			{
				throw new Exception("This request is not supported for this message type!");
			}
		}
		public abstract bool ValidateRequest(IMessageRequest request);

		public string GetId()
		{
			throw new NotImplementedException();
		}
	}
}
