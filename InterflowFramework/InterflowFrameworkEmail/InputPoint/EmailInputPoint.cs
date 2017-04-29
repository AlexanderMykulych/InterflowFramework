using ActiveUp.Net.Mail;
using InterflowFramework.Core.Channel.InputPoint.Const;
using InterflowFramework.Core.Channel.InputPoint.Model;
using InterflowFramework.Core.LinqExtension;
using InterflowFramework.Core.Message;
using InterflowFramework.Core.Message.Model.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace InterflowFrameworkEmail.InputPoint
{
	public class EmailInputPoint : BaseInputPoint
	{
		private Imap4Client Client = new Imap4Client();
		public EmailInputPointConfig Config;
		public Timer Timer;
		private double _timerInterval = 5000;
		public EmailInputPoint(EmailInputPointConfig config) {
			Config = config;
			Timer = new Timer(_timerInterval);
			Timer.Elapsed += OnTimeTick;
		}

		private void OnTimeTick(object sender, ElapsedEventArgs e)
		{
			if (Client != null) {
				//ForDebug
				Timer.Enabled = false;
				var inbox = Client.SelectMailbox("inbox");
				var builder = new StringBuilder();
				var index = 0;
				inbox
					.Search("UNSEEN")
					.Take(10)
					.Select(x => inbox.Fetch.MessageObject(x))
					.ForEach(x =>
					{
						builder
							.AppendFormat("Email message: {0}", ++index)
							.AppendFormat("From: {0}\n", x.From.Email)
							.AppendFormat("Subject: {0}\n", x.Subject)
							.AppendFormat("Body: {0}\n", x.BodyHtml.Text);
					});
				Push(builder.ToString());
			}
		}

		public override void Enable()
		{
			base.Enable();
			if (Config.Ssl)
			{
				Client.ConnectSsl(Config.MailServer, Config.Port);
			}
			else
			{
				Client.Connect(Config.MailServer, Config.Port);
			}
			Client.Login(Config.Login, Config.Password);
			Timer.Enabled = true;
		}
		public override void Dispose()
		{
			base.Dispose();
			Client.Disconnect();
			Timer.Enabled = false;
			Timer.Dispose();
		}
		public override void Push(object message)
		{
			if (message != null && message is string)
			{
				IMessage interflowMessage = CreateTextMessage((string)message);
				Subscriber.Execute(InputPointEvent.onMessage, interflowMessage);
			}
		}
		protected virtual IMessage CreateTextMessage(string text)
		{
			var provider = new TextMessageProvider();
			return provider.Create(text);
		}
	}
}
