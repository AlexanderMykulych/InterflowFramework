﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Channel.Packer.Interface
{
	public interface IPacker: IValidator
	{
		object Pack(object message);
	}
}
