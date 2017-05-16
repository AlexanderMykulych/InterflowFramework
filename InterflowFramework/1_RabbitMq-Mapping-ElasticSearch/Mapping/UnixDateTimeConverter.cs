using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace _1_RabbitMq_Mapping_ElasticSearch.Mapping
{
	public class UnixDateTimeConverter : DateTimeConverterBase
	{
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			
			if (reader.TokenType != JsonToken.Integer && reader.TokenType != JsonToken.Float)
			{
				if (reader.TokenType == JsonToken.Date)
				{
					return (DateTime)reader.Value;
				}
				throw new Exception(
					String.Format("Unexpected token parsing date. Expected Integer, got {0}.",
					reader.TokenType));
			}
			var date = new DateTime(1970, 1, 1);
			if(reader.TokenType == JsonToken.Float) {
				date = date.AddSeconds((double)reader.Value);
			} else {
				date = date.AddSeconds((long)reader.Value);
			}
			return date;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			long ticks;
			if (value is DateTime)
			{
				var epoc = new DateTime(1970, 1, 1);
				var delta = ((DateTime)value) - epoc;
				if (delta.TotalSeconds < 0)
				{
					throw new ArgumentOutOfRangeException(
						"Unix epoc starts January 1st, 1970");
				}
				ticks = (long)delta.TotalSeconds;
			}
			else
			{
				throw new Exception("Expected date object value.");
			}
			writer.WriteValue(ticks);
		}
	}
}
