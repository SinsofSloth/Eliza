using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eliza.Model
{
    class RF5ItemData
    {
		public ItemStorageData Rucksack; // 0x10
		public ItemStorageData ItemBox; // 0x18
		public ItemStorageData Refrigerator; // 0x20
		public ItemStorageData RuneRuck; // 0x28
		public ItemStorageData WeaponBox; // 0x30
		public ItemStorageData ArmorBox; // 0x38
		public ItemStorageData FarmToolBox; // 0x40
		public ItemStorageData RuneBox; // 0x48
		public ItemStorageData ShippingBox; // 0x50
		public FieldOnGroundItemStorage FieldOnGroundItemStorage; // 0x58
	}
}
