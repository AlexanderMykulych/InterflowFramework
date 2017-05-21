using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Runner.GetIpGeoLocation
{
	class Program
	{
		static void Main(string[] args)
		{
			var url = "https://api.calq.io/Track";
			Uri myUri = new Uri(url);
			var ip = Dns.GetHostAddresses(myUri.Host).FirstOrDefault();
			if(ip != null) {
				var result = new WebClient().DownloadString(string.Format("http://ws.cdyne.com/ip2geo/ip2geo.asmx/ResolveIP?ipAddress={0}&licenseKey=1", ip.ToString()));
				var xDoc = XDocument.Parse(result).Root.DescendantNodes().Where(x => x is XElement);
				var res1 = xDoc.FirstOrDefault(x => ((XElement)x).Name.LocalName == "Latitude");
				var res2 = xDoc.Where(x => x is XElement).FirstOrDefault(x => ((XElement)x).Name.LocalName == "Longitude");
				
				Console.WriteLine(result);
			}
			Console.ReadKey();
		}
	}
}
