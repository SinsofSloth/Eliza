using Eliza.Model;
using MessagePack;
using MessagePack.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eliza
{
    class HumanStatusDataFormatter : IMessagePackFormatter<HumanStatusData>
    {
        public HumanStatusData Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            var humanStatusData = new HumanStatusData();

            IFormatterResolver resolver = options.Resolver;
            options.Security.DepthStep(ref reader);

            reader.ReadArrayHeader();

            //StatusDataBase
            humanStatusData.Level = reader.ReadInt32();
            humanStatusData.Exp = reader.ReadInt32();
            humanStatusData.Hp = reader.ReadInt32();
            humanStatusData.Rp = reader.ReadInt32();
            humanStatusData.SaveParameter = resolver.GetFormatterWithVerify<Parameter>().Deserialize(ref reader, options);
            humanStatusData.BadStatus = reader.ReadInt32();
            humanStatusData.TemporaryStatus_UseItem = resolver.GetFormatterWithVerify<ItemData>().Deserialize(ref reader, options);
            humanStatusData.TemporaryStatus_UseItem_Time = reader.ReadInt32();
            humanStatusData.TemporaryStatus_HotSpring = reader.ReadInt32();
            humanStatusData.TemporaryStatus_HotSpring_Time = reader.ReadInt32();
            humanStatusData.TemporaryStatus_Vaccination = reader.ReadInt32();
            humanStatusData.TemporaryStatus_Vaccination_Time = reader.ReadInt32();
            humanStatusData.Score_ATKUp_Level = reader.ReadInt32();
            humanStatusData.Score_ATKUp_Time = reader.ReadInt32();
            humanStatusData.Score_DEFUp_Level = reader.ReadInt32();
            humanStatusData.Score_DEFUp_Time = reader.ReadInt32();

            humanStatusData.ActorID = reader.ReadInt32();
            reader.ReadMapHeader();
            reader.Skip();  //E

            var humanEquip = new HumanEquip();
            humanEquip.EquipItems = resolver.GetFormatterWithVerify<ItemData[]>().Deserialize(ref reader, options);
            humanStatusData.HumanEquip = humanEquip;
            humanStatusData.PartnerMovementOrderType = reader.ReadInt32();
            humanStatusData.DualSkillGauge = (float)reader.ReadDouble();

            return humanStatusData;
        }

        public void Serialize(ref MessagePackWriter writer, HumanStatusData value, MessagePackSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
