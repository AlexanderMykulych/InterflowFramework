using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFrameworkEmail.InputPoint
{
	public class EmailInputPointConfig
	{
		public string MailServer;
		public int Port;
		public bool Ssl;
		public string Login;
		public string Password;
		public EmailInputPointConfig(string mailServer, int port, bool ssl, string login, string password) {
			MailServer = mailServer;
			Port = port;
			Ssl = ssl;
			Login = login;
			Password = password;
		}
	}
}
