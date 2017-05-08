using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Channel.Transport.Const
{
	public static class TransportEvent
	{
		public static readonly string onConnect = @"connect";
		public static readonly string onDisconnect = @"disconnect";
		public static readonly string onMessage = @"message";
	}
}
