using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eliza.Model
{
    class RF5StatusData
    {
        public readonly Dictionary<ActorID, HumanStatusData> HumanStatusDatas;
        public readonly List<FriendMonsterStatusData> FriendMonsterStatusDatas;
    }
}
