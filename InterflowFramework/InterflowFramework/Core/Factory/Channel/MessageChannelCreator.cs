using InterflowFramework.Core.Channel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterflowFramework.Core.Channel;

namespace InterflowFramework.Core.Factory.Channel
{
	public class MessageChannelCreator : ChannelCreator<MessageChannel, MessageChannelCreator>
	{
		public override IChannel Create()
		{
			return new MessageChannel()
			{
				InputPoints = _inPoints,
				OutputPoints = _outPoints,
				Transport = _transport
			};
		}
	}
}
