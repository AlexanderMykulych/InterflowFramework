using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Channel.InputPoint.Const
{
	public static class InputPointEvent
	{
		public static readonly string onMessage = @"message";
		public static readonly string onResponse = @"response";
		public static readonly string onConnect = @"connect";
		public static readonly string onDisconnect = @"disconnect";
		
	}
}
