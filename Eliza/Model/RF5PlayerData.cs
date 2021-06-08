using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eliza.Model
{
    class RF5PlayerData
    {
		public int PlayerGold;
		public string PlayerName;
		public bool IsPlayerMale;
		public bool IsPlayerModelMale;
		public VariationNo VariationNo;
		public int PlayerBirthDay;
		public NPCID MarriedNPCID;
		public int SeedPoint;
		public PoliceRank PoliceRank;
		public int Stone;
		public int Lumber;
		public int Compost;
		public int Esa;
		public int DailyRecipePan_Bakery;
		public int DailyRecipePan_Restaurant;
		public int BathroomBlocked;
		public SkillData[] SkillDatas;
		public ActorID DualSmithActor;
		public ActorID DualCookActor;
		public ActorID DualFishingActor;
	}
}
