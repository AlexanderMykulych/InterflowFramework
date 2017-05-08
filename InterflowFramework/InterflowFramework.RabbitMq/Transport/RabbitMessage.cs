using InterflowFramework.Core.Channel.Identified;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.RabbitMq.Transport
{
	public class RabbitMessage
	{
		public string Data;
		public string Type;
		public string ResponseTo;
		public TRabbitMessageMode Mode = TRabbitMessageMode.Message;
	}
	public enum TRabbitMessageMode {
		Message = 0,
		Response = 1
	}
}
