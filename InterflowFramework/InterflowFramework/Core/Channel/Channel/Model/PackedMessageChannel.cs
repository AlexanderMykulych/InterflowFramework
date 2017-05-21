using InterflowFramework.Core.Channel.Model;
using InterflowFramework.Core.Channel.Packer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Channel.Channel.Model
{
	public class PackedMessageChannel: MessageChannel
	{
		private IPacker _packer;
		private IPacker _responsePacker;
		private IUnpacker _unpacker;
		private IUnpacker _responseUnpacker;
		public PackedMessageChannel SetPaker(IPacker packer) {
			_packer = packer;
			return this;
		}
		public PackedMessageChannel SetResponsePaker(IPacker packer) {
			_responsePacker = packer;
			return this;
		}
		public PackedMessageChannel SetUnpaker(IUnpacker unpacker)
		{
			_unpacker = unpacker;
			return this;
		}
		public PackedMessageChannel SetResponseUnpaker(IUnpacker unpacker)
		{
			_responseUnpacker = unpacker;
			return this;
		}
		public PackedMessageChannel SetPackagers(IPacker packer, IUnpacker unpacker, IPacker responsePacker, IUnpacker responseUnpacker) {
			return this
				.SetPaker(packer)
				.SetUnpaker(unpacker)
				.SetResponsePaker(responsePacker)
				.SetResponseUnpaker(responseUnpacker);
		}
		protected override void onInputPointMessage(object obj)
		{
			if(_packer != null) {
				obj = _packer.Pack(obj);
			}
			base.onInputPointMessage(obj);
		}
		protected override void onOutputPointResponse(object obj)
		{
			if (_responsePacker != null)
			{
				obj = _responsePacker.Pack(obj);
			}
			base.onOutputPointResponse(obj);
		}
		protected override void OnTransportMessage(object message)
		{
			if(_unpacker != null) {
				message = _unpacker.Unpack(message);
			}
			base.OnTransportMessage(message);
		}
		protected override void OnTransportResponse(object obj)
		{
			if(_responseUnpacker != null) {
				_responseUnpacker.Unpack(obj);
			}
			base.OnTransportResponse(obj);
		}
	}
}
