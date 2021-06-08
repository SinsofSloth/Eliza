using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eliza.Model
{
    class RF5EventData
    {
		public EventSaveParameter EventSaveParameter;
		public SaveFlagStorage SaveFlag;
		public SubEventSaveData SubEventSaveDatas;
		public int MainScenarioStep;
		public List<ActorID> PresentSendActorList;
		public List<ActorID> PresentRecvActorList;
		public bool IsStartFishing;
		public List<ActorID> FesJoinActorList;
		public List<ActorID> FesVisitorActorList;
		public List<FesNpcScore> FesNpcScoreList;
		public int IsCalcFesId;
		public int VictoryCandidateNpcId;
		public int JudgeChildId;
		public List<int> FishTypeList;
		private Dictionary<int, int> FlagDataMappings;
	}
}
