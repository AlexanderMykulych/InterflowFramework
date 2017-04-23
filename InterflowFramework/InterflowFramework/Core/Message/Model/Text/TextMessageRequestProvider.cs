﻿using InterflowFramework.Core.Message.Interface;
using InterflowFramework.Core.Message.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Message.Model.Text
{
	public class TextMessageRequestProvider : BaseMessageRequestProvider
	{
		protected override IMessageRequest CreateRequest()
		{
			return new TextMessageRequest();
		}
	}
}
