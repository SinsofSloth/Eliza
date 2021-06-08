using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eliza.Model
{
    class RF5ShippingData
    {
		public int completedPercent;
		public long totalIncome;
		public List<ShipmentItemRecords>[] ItemRecordList;
		public List<FishShipmentRecords> FishRecordList;
		public List<SeedLevelRecords> SeedLevelRecordList;
		private readonly int[] CategoryMaxTable;
	}
}
