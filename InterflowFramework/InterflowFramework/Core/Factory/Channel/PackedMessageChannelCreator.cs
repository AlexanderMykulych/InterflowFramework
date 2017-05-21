﻿using InterflowFramework.Core.Channel.Channel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterflowFramework.Core.Channel;
using InterflowFramework.Core.Channel.Packer.Interface;

namespace InterflowFramework.Core.Factory.Channel
{
	public class PackedMessageChannelCreator : ChannelCreator<PackedMessageChannel, PackedMessageChannelCreator>
	{
		private Action<PackedMessageChannel> _packerSetter;
		public PackedMessageChannelCreator Packers(IPacker packer = null, IUnpacker unpacker = null, IPacker responsePacker = null, IUnpacker responseUnpacker = null) {
			_packerSetter = channel => channel.SetPackagers(packer, unpacker, responsePacker, responseUnpacker);
			return this;
		}
		public override IChannel Create()
		{
			var channel = new PackedMessageChannel()
			{
				InputPoints = _inPoints,
				OutputPoints = _outPoints,
				Transport = _transport
			};
			if (_packerSetter != null) {
				_packerSetter(channel);
			}
			return channel;
		}
	}
}
