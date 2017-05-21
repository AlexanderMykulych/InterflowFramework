using Nest;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _1_RabbitMq_Mapping_ElasticSearch.Mapping
{
	public static class GeoUtil
	{
		public static Dictionary<string, GeoLocation> Memory = new Dictionary<string, GeoLocation>();
		public static GeoLocation GetByUrl(string url) {
			Uri myUri = new Uri(url);
			var ip = Dns.GetHostAddresses(myUri.Host).FirstOrDefault();
			if(ip == null) {
				return new GeoLocation(0, 0);
			}
			return GetByIp(ip.ToString());
		}
		public static GeoLocation GetByIp(string ip) {
			if(Memory.ContainsKey(ip)) {
				return Memory[ip];
			}
			var result = new WebClient().DownloadString(string.Format("http://ws.cdyne.com/ip2geo/ip2geo.asmx/ResolveIP?ipAddress={0}&licenseKey=1", ip.ToString()));
			var xDoc = XDocument.Parse(result).Root.DescendantNodes().Where(x => x is XElement);
			var lat = xDoc.FirstOrDefault(x => ((XElement)x).Name.LocalName == "Latitude");
			var lng = xDoc.Where(x => x is XElement).FirstOrDefault(x => ((XElement)x).Name.LocalName == "Longitude");
			var res = new GeoLocation(
				double.Parse((lat as XElement).Value, CultureInfo.InvariantCulture),
				double.Parse((lng as XElement).Value, CultureInfo.InvariantCulture)
			);
			Memory.Add(ip, res);
			return res;
		}
	}
}
