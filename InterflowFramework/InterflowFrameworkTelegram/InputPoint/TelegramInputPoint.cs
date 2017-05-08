using InterflowFramework.Core.Channel.InputPoint.Const;
using InterflowFramework.Core.Channel.InputPoint.Model;
using InterflowFramework.Core.Message;
using InterflowFramework.Core.Message.Model.Text;
using InterflowFrameworkTelegram.InputPoint.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace InterflowFrameworkTelegram.InputPoint
{
	public class TelegramInputPoint : BaseInputPoint
	{
		public TelegramInputPointConfig Config;
		public TelegramBotClient Bot;
		public bool Enabled = false;
		public TelegramInputPoint(TelegramInputPointConfig config) {
			Config = config;
			Bot = new TelegramBotClient(config.ApiKey);
			
		}
		private void OnBotMessage(object sender, MessageEventArgs e)
		{
			var message = e.Message;
			if(message != null && message.Type == MessageType.TextMessage) {
				Push(message.Text);
			}
		}
		public override void Disable()
		{
			if (Enabled)
			{
				base.Disable();
				Bot.OnMessage -= OnBotMessage;
				Bot.StopReceiving();
				Enabled = false;
			}
		}
		public override void Enable()
		{
			if (!Enabled)
			{
				base.Enable();
				Bot.OnMessage -= OnBotMessage;
				Bot.OnMessage += OnBotMessage;
				Bot.StartReceiving();
				Enabled = true;
			}
		}
		public override void Push(object message)
		{
			if (message != null && message is string)
			{
				IMessage interflowMessage = CreateTextMessage((string)message);
				Subscriber.Execute(InputPointEvent.onMessage, interflowMessage);
			}
		}
		protected virtual IMessage CreateTextMessage(string text) {
			var provider = new TextMessageProvider();
			return provider.Create(text);
		}
	}
}
