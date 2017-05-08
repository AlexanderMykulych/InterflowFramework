﻿using InterflowFramework.Core.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Channel.Transport.Interface
{
	public interface IMessageTransport: ITransport
	{
		void Push(IMessage message);
	}
}