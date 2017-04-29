using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFrameworkTelegram.InputPoint.Config
{
	public class TelegramInputPointConfig
	{
		public TelegramInputPointConfig(string apiKey) {
			ApiKey = apiKey;
		}
		public string ApiKey;
	}
}
