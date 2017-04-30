using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalFramework.Tcp.Transport
{
	public class TcpTransportConfig
	{
		public string Host;
		public int Port;

		public TcpTransportConfig(string host, int port) {
			Host = host;
			Port = port;
		}
		public bool isNotEmpty {
			get {
				return !string.IsNullOrEmpty(Host) && Port > 0;
			}
		}
	}
}
