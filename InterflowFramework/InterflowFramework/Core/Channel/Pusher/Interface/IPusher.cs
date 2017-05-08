﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Channel.Pusher.Interface
{
	public interface IPusher
	{
		void Push(object message);
		void Message(object message);
	}
}
