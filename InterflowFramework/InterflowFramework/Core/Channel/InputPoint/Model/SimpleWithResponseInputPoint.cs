using InternalFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Channel.InputPoint.Model
{
	public class SimpleWithResponseInputPoint: SimpleInputPoint
	{
		private Action<object> _onMessage;
		public SimpleWithResponseInputPoint(Action<object> onMessage) {
			_onMessage = onMessage;
		}
		public override void Push(object message)
		{
			base.Push(new MessagePackage(message, GetId()));
		}
		public override void Message(object message)
		{
			if (_onMessage != null)
			{
				_onMessage(message);
			}
		}
	}
}
