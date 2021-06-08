using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eliza.Model
{
    class RF5FarmData
    {
		public bool[] FirstVisitFarm;
		public int[] FarmSizeLevels;
		public FarmCropData[] FarmCropDatas;
		public int[] CrystalUseCounts;
		public FarmManager.RF4_CROP_STATE[] Crop;
		public FarmManager.RF4_SOIL_INFO[] Soil;
		public int[] HarvestCount;
		public List<ItemID> ItemHarvestIdList;
		public bool[] MonsterHutReleaseFlags;
		public MonsterHutSaveData[] MonsterHutSaveDatas;
	}
}
