using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eliza.Model
{
    class SaveDataHeader
    {
		public ulong UID; // 0x10
		public uint version; // 0x18
		public char[] project; // 0x20
		public uint WCnt; // 0x28
		public uint WOpt; // 0x2C
		private SaveTime saveTime; // 0x30
	}
}
