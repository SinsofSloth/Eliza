using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eliza.Model
{
    class RF5NPCData
    {
		public List<NpcSaveParameter> NpcSaveParameters; // 0x10
		public NpcDateSaveParameter NpcDateSaveParam; // 0x18
		public ChildSaveData ChildSaveDatas; // 0x20
		public GiveBirthSaveParameter GiveBirthParams; // 0x28
		public Dictionary<ActorID, ItemStorageData> NpcHatCache; // 0x30
	}
}
