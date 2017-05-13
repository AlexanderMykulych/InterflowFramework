using InterflowFramework.Core.Channel.OutputPoint.Const;
using InterflowFramework.Core.Channel.OutputPoint.Model;
using InternalFramework.Nancy.InputPoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalFramework.Nancy.OutputPoint
{
	public class ResponseOutputPoint: BaseOutputPoint
	{
		public ResponseOutputPoint(params Func<object, object>[] actions)
		{
			actions.All(action => {
				On(OutputPointEvent.OnMessageRecived, message =>
				{
					Response(message, action(message));
				});
				return true;
			});
		}
		public override void Push(object message)
		{
			Subscriber.Execute(OutputPointEvent.OnMessageRecived, message);
		}
		public virtual void Response(object inputMessage, object responseMessage) {
			var msg = (MessagePackage)inputMessage;
			msg.Object = responseMessage;
			Subscriber.Execute(OutputPointEvent.OnResponse, msg);
		}
	}
}
