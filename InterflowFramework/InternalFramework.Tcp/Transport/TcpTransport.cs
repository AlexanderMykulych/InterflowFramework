using InterflowFramework.Core.Channel.Transport.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalFramework.Tcp.Transport
{
	public class TcpTransport : BaseTransport
	{
		TcpTransportConfig Config;
		TcpClient Client;
		public TcpTransport(TcpTransportConfig config) {
			Config = config;
		}
		public override void Push(object message)
		{
			throw new NotImplementedException();
		}
		public override void Enable()
		{
			base.Enable();
			Connect();
		}
		public override void Disable()
		{
			base.Disable();
			Client.Close();
		}
		protected virtual void Connect() {
			if(Config.isNotEmpty) {
				Client = new TcpClient();
				Client.Connect(Config.Host, Config.Port);
			}
		}
	}
}
