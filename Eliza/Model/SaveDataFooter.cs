using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eliza.Model
{
    class SaveDataFooter
    {
		public int BodyLength; // 0x10
		public int Length; // 0x14
		public uint Sum; // 0x18
		public int Blank; // 0x1C
	}
}
