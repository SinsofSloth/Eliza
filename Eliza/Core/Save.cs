using Eliza.Core.Serialization;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using System.Collections.Generic;
using System.IO;
using System.Text;

//TODO:
//Generate JSON:
//https://github.com/SinsofSloth/RF5-global-metadata/blob/1a4dc9fb55263296c8a8564176591a7ab9fa1745/_no_namespace/FarmManager.FARM_ID.cs
//https://github.com/SinsofSloth/RF5-global-metadata/blob/1a4dc9fb55263296c8a8564176591a7ab9fa1745/_no_namespace/CropID.cs
//https://github.com/SinsofSloth/RF5-global-metadata/blob/1a4dc9fb55263296c8a8564176591a7ab9fa1745/_no_namespace/MineTypeID.cs
//https://github.com/SinsofSloth/RF5-global-metadata/blob/1a4dc9fb55263296c8a8564176591a7ab9fa1745/_no_namespace/ItemID.cs
//https://github.com/SinsofSloth/RF5-global-metadata/blob/1a4dc9fb55263296c8a8564176591a7ab9fa1745/Define/VariationNo.cs
//https://github.com/SinsofSloth/RF5-global-metadata/blob/1a4dc9fb55263296c8a8564176591a7ab9fa1745/Define/NPCID.cs
//https://github.com/SinsofSloth/RF5-global-metadata/blob/1a4dc9fb55263296c8a8564176591a7ab9fa1745/Define/PoliceRank.cs
//https://github.com/SinsofSloth/RF5-global-metadata/blob/1a4dc9fb55263296c8a8564176591a7ab9fa1745/Define/ActorID.cs
//https://github.com/SinsofSloth/RF5-global-metadata/blob/1a4dc9fb55263296c8a8564176591a7ab9fa1745/Define/Season.cs
//https://github.com/SinsofSloth/RF5-global-metadata/blob/1a4dc9fb55263296c8a8564176591a7ab9fa1745/Define/TimeZone.cs
//https://github.com/SinsofSloth/RF5-global-metadata/blob/1a4dc9fb55263296c8a8564176591a7ab9fa1745/Define/Weather.cs
//https://github.com/SinsofSloth/RF5-global-metadata/blob/1a4dc9fb55263296c8a8564176591a7ab9fa1745/Define/NpcEventType.cs
////https://github.com/SinsofSloth/RF5-global-metadata/blob/1a4dc9fb55263296c8a8564176591a7ab9fa1745/_no_namespace/EventProceedType.cs
//https://github.com/SinsofSloth/RF5-global-metadata/blob/1a4dc9fb55263296c8a8564176591a7ab9fa1745/Define/EventScriptID.cs
//https://github.com/SinsofSloth/RF5-global-metadata/blob/1a4dc9fb55263296c8a8564176591a7ab9fa1745/Define/EventPointID.cs
//https://github.com/SinsofSloth/RF5-global-metadata/blob/1a4dc9fb55263296c8a8564176591a7ab9fa1745/_no_namespace/EventCheckType.cs
//https://github.com/SinsofSloth/RF5-global-metadata/blob/1a4dc9fb55263296c8a8564176591a7ab9fa1745/Define/IconType.cs
//https://github.com/SinsofSloth/RF5-global-metadata/blob/1a4dc9fb55263296c8a8564176591a7ab9fa1745/Define/IconViewType.cs
//https://github.com/SinsofSloth/RF5-global-metadata/blob/1a4dc9fb55263296c8a8564176591a7ab9fa1745/Define/GameFlagData.cs
//https://github.com/SinsofSloth/RF5-global-metadata/blob/d89ff0d373c436b086dde5248049073d24bc85ff/Field/FieldSceneId.cs
//https://github.com/SinsofSloth/RF5-global-metadata/blob/1a4dc9fb55263296c8a8564176591a7ab9fa1745/Define/Place.cs
//https://github.com/SinsofSloth/RF5-global-metadata/blob/1a4dc9fb55263296c8a8564176591a7ab9fa1745/Define/NpcEventType.cs
//https://github.com/SinsofSloth/RF5-global-metadata/search?q=DatProgressType
//DateType
//DateSpotID
//ItemID
//FarmManager.RF4_CROP_GROW_STATE 
//Return List for arrays in properties
namespace Eliza.Core
{
    public class Save
    {
        public static Model.Save Read(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
            using (var ms = new MemoryStream())
            {
                fs.CopyTo(ms);
                Decrypt(ms);

                var deserializer = new BinaryDeserializer(ms);
                var save = deserializer.Deserialize<Model.Save>();

                //Read until end for now
                var pos = deserializer.BaseStream.Position;
                var reader = deserializer.Reader;

                save.saveData.Unmapped = reader.ReadBytes((int)(new FileInfo(path).Length - (pos + 0x10)));

                save.Footer = new Model.SaveData.SaveDataFooter();
                save.Footer.BodyLength = reader.ReadInt32(); // header + body - (footer + unmapped)
                save.Footer.Length = reader.ReadInt32();    // everything - footer
                save.Footer.Sum = reader.ReadUInt32();
                save.Footer.Blank = reader.ReadInt32();

                //using (var test = new FileStream("test", FileMode.Create, FileAccess.Write))
                //{
                //    var serializer = new BinarySerializer(test);
                //    serializer.Serialize(save);
                //}
                return save;
            };
        }

        //It look likes unmapped is just some random padding to get it aligned to 256
        public static void Write(string path, Model.Save save)
        {
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            {
                //var serializer = new BinarySerializer(fs);
                //serializer.Serialize(save);
                //var writer = serializer.Writer;

                //fs.SetLength((fs.Position + 0xFF) & ~0xFF);
                //var dataSize = fs.Length;

                ////Read Header to gen sum
                //fs.Seek(0x0, SeekOrigin.Begin);
                //byte[] header = new byte[0x20];
                //fs.Read(header, 0, header.Length);

                //byte[] data = new byte[dataSize];
                //fs.Read(data, 0, data.Length);

                ////var encryptedData = Encrypt(data);

                ////fs.Seek(0x20, SeekOrigin.Begin);
                ////fs.Write(encryptedData);

                //var combinedData = new List<byte>();
                //combinedData.AddRange(header);
                //combinedData.AddRange(data);
                ////combinedData.AddRange(encryptedData);
                //var checksum = Checksum(combinedData.ToArray());

                //writer.Write(combinedData.Count - (0x10 + save.saveData.Unmapped.Length));
                //writer.Write(combinedData.Count);
                //writer.Write(checksum);
                //writer.Write(save.Footer.Blank);


                var serializer = new BinarySerializer(fs);
                serializer.Serialize(save);
                var writer = serializer.Writer;

                fs.SetLength((fs.Position + 0xFF) & ~0xFF);
                var dataSize = fs.Length;

                //Read Header to gen sum
                fs.Seek(0x0, SeekOrigin.Begin);
                byte[] header = new byte[0x20];
                fs.Read(header, 0, header.Length);

                byte[] data = new byte[dataSize];
                fs.Read(data, 0, data.Length);

                var encryptedData = Encrypt(data);

                fs.Seek(0x20, SeekOrigin.Begin);
                fs.Write(encryptedData);

                var combinedData = new List<byte>();
                combinedData.AddRange(header);
                combinedData.AddRange(encryptedData);
                var checksum = Checksum(combinedData.ToArray());

                writer.Write(combinedData.Count - (0x10 + save.saveData.Unmapped.Length));
                writer.Write(combinedData.Count);
                writer.Write(checksum);
                writer.Write(save.Footer.Blank);
            }
        }

        static byte[] Encrypt(byte[] data)
        {
            return RijndaelCrypto(data, true);
        }

        static byte[] Decrypt(byte[] data)
        {
            return RijndaelCrypto(data, false);
        }

        static void Decrypt(MemoryStream ms)
        {
            var pos = ms.Seek(0x20, SeekOrigin.Begin);
            byte[] encryptedData = new byte[ms.Length - (32 + 16)];
            ms.Read(encryptedData, 0, encryptedData.Length);
            ms.Seek(pos, SeekOrigin.Begin);

            var decryptedData = Decrypt(encryptedData);
            ms.Write(decryptedData, 0, decryptedData.Length);
            ms.Seek(0, SeekOrigin.Begin);
        }

        static byte[] RijndaelCrypto(byte[] data, bool isEncrypting)
        {
            //RF5 uses 256bit block Rijndael Encryption
            var aesKey = Encoding.UTF8.GetBytes("1cOSvkZ4HQCi6z/yQpEEl4neB+AIXwTX");
            var aesIV = Encoding.UTF8.GetBytes("XuMigxpK61gLwgo1RsreLLGPcw3vJFze");
            var engine = new RijndaelEngine(256);
            var blockCipher = new CbcBlockCipher(engine);
            var cipher = new BufferedBlockCipher(blockCipher);
            var keyParam = new KeyParameter(aesKey);
            var keyParamWithIV = new ParametersWithIV(keyParam, aesIV, 0, 32);

            cipher.Init(isEncrypting, keyParamWithIV);
            var output = new byte[cipher.GetOutputSize(data.Length)];
            var length = cipher.ProcessBytes(data, output, 0);
            cipher.DoFinal(output, length);
            return output;
        }

        static uint Checksum(byte[] buffer)
        {
            uint sum = 0xcbf29ce4;
            uint lo = 0;
            uint running_sum = 0x39;
            for (int index = 0; index < buffer.Length; index++)
            {
                uint value = buffer[index];
                uint delta = value - lo;
                lo = (lo & 0xff) + 0xb2;
                sum = sum * 0x1b3 ^ (delta ^ running_sum) & 0xff;
                running_sum = value;
            }
            return sum;
        }
    }
}
