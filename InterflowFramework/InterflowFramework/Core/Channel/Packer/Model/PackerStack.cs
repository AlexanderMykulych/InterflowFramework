using InterflowFramework.Core.Channel.Packer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace InterflowFramework.Core.Channel.Packer.Model
{
	public abstract class PackerStack : IPacker, IEnumerable<IPacker>, IEnumerator<IPacker>
	{
		private IPacker[] _packer;
		private int _currentIndex;
		public PackerStack(params IPacker[] packer) {
			_packer = packer;
			_currentIndex = 0;
		}

		public IPacker Current => _packer[_currentIndex];

		object IEnumerator.Current => Current;

		public void Dispose()
		{
			_packer = null;
		}

		public IEnumerator<IPacker> GetEnumerator()
		{
			return this;
		}

		public bool MoveNext()
		{
			if(_currentIndex < _packer.Length) {
				_currentIndex++;
				return true;
			}
			return false;
		}

		public void Reset()
		{
			_currentIndex = 0;
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return (IEnumerator<IPacker>)GetEnumerator();
		}
		public abstract bool Valide(object obj);
		public abstract object Pack(object message);
	}
}
