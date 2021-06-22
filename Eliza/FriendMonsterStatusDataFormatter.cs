using Eliza.Model;
using MessagePack;
using MessagePack.Formatters;

namespace Eliza
{
    class FriendMonsterStatusDataFormatter : IMessagePackFormatter<FriendMonsterStatusData>
    {
        public FriendMonsterStatusData Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            var friendMonsterStatusData = new FriendMonsterStatusData();

            IFormatterResolver resolver = options.Resolver;
            options.Security.DepthStep(ref reader);

            reader.ReadArrayHeader();

            //StatusDataBase
            friendMonsterStatusData.Level = reader.ReadInt32();
            friendMonsterStatusData.Exp = reader.ReadInt32();
            friendMonsterStatusData.Hp = reader.ReadInt32();
            friendMonsterStatusData.Rp = reader.ReadInt32();
            friendMonsterStatusData.SaveParameter = resolver.GetFormatterWithVerify<Parameter>().Deserialize(ref reader, options);
            friendMonsterStatusData.BadStatus = reader.ReadInt32();
            friendMonsterStatusData.TemporaryStatus_UseItem = resolver.GetFormatterWithVerify<ItemData>().Deserialize(ref reader, options);
            friendMonsterStatusData.TemporaryStatus_UseItem_Time = reader.ReadInt32();
            friendMonsterStatusData.TemporaryStatus_HotSpring = reader.ReadInt32();
            friendMonsterStatusData.TemporaryStatus_HotSpring_Time = reader.ReadInt32();
            friendMonsterStatusData.TemporaryStatus_Vaccination = reader.ReadInt32();
            friendMonsterStatusData.TemporaryStatus_Vaccination_Time = reader.ReadInt32();
            friendMonsterStatusData.Score_ATKUp_Level = reader.ReadInt32();
            friendMonsterStatusData.Score_ATKUp_Time = reader.ReadInt32();
            friendMonsterStatusData.Score_DEFUp_Level = reader.ReadInt32();
            friendMonsterStatusData.Score_DEFUp_Time = reader.ReadInt32();

            friendMonsterStatusData.DataID = reader.ReadUInt32();
            friendMonsterStatusData.Name = reader.ReadString();
            friendMonsterStatusData.MonsterDataID = reader.ReadInt32();
            reader.ReadMapHeader();
            reader.Skip(); //LovePoint
            friendMonsterStatusData.LovePoint = reader.ReadInt32();
            friendMonsterStatusData.TimeStamp = reader.ReadInt32();
            friendMonsterStatusData.FarmId = reader.ReadInt32();
            friendMonsterStatusData.HouseId = reader.ReadInt32();
            friendMonsterStatusData.RoomId = reader.ReadInt32();
            friendMonsterStatusData.PartyNo = reader.ReadInt32();
            friendMonsterStatusData.PartnerMovementOrderType = reader.ReadInt32();
            friendMonsterStatusData.FarmMonsterOrder = reader.ReadInt32();
            friendMonsterStatusData.FarmFieldWorkArea = reader.ReadInt32();
            friendMonsterStatusData.IsBrushed = reader.ReadBoolean();
            friendMonsterStatusData.IsPresent = reader.ReadBoolean();
            friendMonsterStatusData.EsaGet = reader.ReadBoolean();
            friendMonsterStatusData.IsWorkedToday = reader.ReadBoolean();
            friendMonsterStatusData.IsSeededToday = reader.ReadBoolean();
            friendMonsterStatusData.IsYieldToday = reader.ReadBoolean();
            friendMonsterStatusData.Bonus_HP = reader.ReadInt32();
            friendMonsterStatusData.Bonus_STR = reader.ReadInt32();
            friendMonsterStatusData.Bonus_INT = reader.ReadInt32();
            friendMonsterStatusData.Bonus_VIT = reader.ReadInt32();
            return friendMonsterStatusData;

        }

        public void Serialize(ref MessagePackWriter writer, FriendMonsterStatusData value, MessagePackSerializerOptions options)
        {
            throw new System.NotImplementedException();
        }
    }
}
