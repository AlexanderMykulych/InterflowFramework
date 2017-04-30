using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalFramework.NetMQ.Transport
{
	[ProtoContract]
	public class NetMqMessage
	{
		[ProtoMember(1)]
		public string Text;
	}
}
