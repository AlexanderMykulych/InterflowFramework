using Fleck;
using InterflowFramework.Core.Channel.InputPoint.Const;
using InterflowFramework.Core.Channel.InputPoint.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalFramework.Fleck.InputPoint
{
	public class FleckInputPoint : SimpleInputPoint
	{
		public string ConnectionString;
		private WebSocketServer _server;
		public WebSocketServer Server {
			get {
				if(_server == null) {
					_server = new WebSocketServer(ConnectionString);
				}
				return _server;
			}
		}
		public FleckInputPoint(string connectionString): base() {
			ConnectionString = connectionString;
		}
		public override void Enable()
		{
			base.Enable();
			Server.Start(WebSocketServerConfigurate);
		}
		public override void Dispose()
		{
			base.Dispose();
			Server.Dispose();
		}
		private void WebSocketServerConfigurate(IWebSocketConnection obj)
		{
			obj.OnOpen = () => Subscriber.Execute(InputPointEvent.onConnect);
			obj.OnClose = () => Subscriber.Execute(InputPointEvent.onDisconnect);
			obj.OnMessage = msg => Push(msg);
		}
	}
}
