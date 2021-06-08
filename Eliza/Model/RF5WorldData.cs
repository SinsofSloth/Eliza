using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eliza.Model
{
    class RF5WorldData
    {
		private byte DifficultyValue;
		public int SenarioStoppedTime;
		public int MapId;
		public Vector3 Position;
		public Vector3 RotationEuler;
		public int InGameTimeElapsedTime;
		public WeatherData WeatherData;
		public uint ShopRandSeedFix;
		public uint ShopRandSeed;
		public int ShopSeedGenerateDay;
		public int LastShippingDay;
		public int LastPlaceId;
		public int LastSleepTime;
		public MiningPointSaveData[] MiningPointSaveDatas;
		public RewardBoxSaveData RewardBoxSaveData;
		public SaveFlagStorage ItemSpawnFlag;
		public SaveFlagStorage TreasureFlag;
		public SaveFlagStorage GimmickFlag;
		public int SeedPointElapsedDay;
		public int SeedPointMonsterAddedCount;
		public float SeedSupportCoolTime;
		public List<int> MeteorPosition;
		public int OffsetFiveYearAgo;
		public int PunchCount;
	}
}
