using InterflowFramework.Core.Channel.InputPoint;
using InterflowFramework.Core.Channel.Model;
using InterflowFramework.Core.Channel.OutputPoint;
using InterflowFramework.Core.Channel.OutputPoint.Const;
using InterflowFramework.Core.Channel.OutputPoint.Model.TestPoint;
using InterflowFramework.Core.Channel.Transport.Model;
using InterflowFramework.Core.Channel.Transport.Model.Inline;
using InterflowFramework.Core.Message.Model.Text;
using InterflowFrameworkEmail.InputPoint;
using InterflowFrameworkRunner.TestProvider;
using InterflowFrameworkTelegram.InputPoint;
using InterflowFrameworkTelegram.InputPoint.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFrameworkRunner.TestCase.Channel.BaseTestOnText
{
	public class TelegramTest : ITest
	{
		public MessageChannel Channel;
		public void Execute()
		{
			var OutPoint = new TestOutpuPoint();
			OutPoint.On(OutputPointEvent.OnMessageRecived, onMessage);
			Channel = new MessageChannel()
			{
				Transport = new InlineTransport(),
				InputPoints = new List<IInputPoint>() {
					new TelegramInputPoint(new TelegramInputPointConfig("368717724:AAFkTcAtUnKXihwTiHqwAuCdXEvN6USz4Pk")),
					new EmailInputPoint(new EmailInputPointConfig("imap.gmail.com", 993, true, "alexander.mykulych@gmail.com", "19960121sasha"))
				},
				OutputPoints = new List<IOutputPoint>() { OutPoint }
			};
			Channel.Enable();
		}
		public void onMessage(object message) {
			if(message is TextMessage) {
				Console.WriteLine(((TextMessage)message).Text);
			}
		}
	}
}
