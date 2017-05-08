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
	public class ReturnOutputPoint : BaseOutputPoint
	{
		public string Text;
		public ReturnOutputPoint(string text) {
			Text = text;
		}
		public override void Push(object message)
		{
			Console.WriteLine(message);
			var msg = (MessagePackage)message;
			msg.Object = Text;
			Subscriber.Execute(OutputPointEvent.OnResponse, msg);
		}
	}
}
