using InterflowFramework.Core.Channel.InputPoint.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFrameworkRunner.TestCase.Channel
{
	public static class NancyChannel
	{
		public static SimpleInputPoint point;
		public static void Push(object message) {
			if(point != null) {
				point.Push(message);
			}
		}
	}
}
